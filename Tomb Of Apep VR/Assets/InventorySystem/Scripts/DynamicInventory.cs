using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GVRI{
    public class DynamicInventory : Inventory
    {
        public Inserter inserter;
        public GameObject invSlotPrefab;
        public uint maxSlots = 5;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            if (!inserter)
            {//the inserter is the point where items will get put into
                Debug.LogError("[DynamicInventory] Kind of pointless without an Inserter");
            }
            else
            {
                inserter.target = InsertionIntoInserter;
            }

            if (!invSlotPrefab) Debug.LogError("[DynamicInventory] Will not be able to create any new Slots, because the invSlotPrefab is missing");
        }

        public virtual void SlotChanged(core.Slot slot)
        {
            //this function is only called from inventoryslots belonging to this inventory
            if (slot.ItemCount == 0)
            {//the inventory slot is empty
                coreInv.slots.Remove(slot);//remove slot from inventory
                Slot bs = FindSlotWithSlot(slot);
                if(bs != null)
                {
                    slots.Remove(bs);// remove inventoryslot that references the slot 
                    bs.gameObject.SetActive(false); //deactivate, so it won't get picked up by updateInventorySlots() anymore
                    Destroy(bs.gameObject); //remember, this gets called very late in the unity queue
                }

                UpdateSlots();
            }
        }

        public virtual Slot InsertionIntoInserter(Inserter inserter, Item i)
        {
            core.ItemInfo itemInfo = i.itemInfo;

            // you cannot store more different items, so this will result in the inserter filling up
            if (coreInv.slots.Count - 1 >= maxSlots) return null;

            //check whether there is an inventorySlot that contains the specified item
            Slot bs = FindSlotWithItem(itemInfo);
            if (bs != null)
            {//prepare for transfer
                return bs;
            }

            //program got here, meaning it didn't find any inventory Slot containing that item
            //creating new slot
            if (invSlotPrefab)
            {//invSlotPrefab has been specified so everything is good to go
                GameObject go = Instantiate(invSlotPrefab);
                go.transform.parent = transform;//do not put this into instantiate if you want to keep prefab scaling
                bs = go.GetComponent<Slot>();
                if (bs == null)
                {//this most likely is because the user forgot to add one
                    Debug.LogError("[DynamicInventory] The SlotPrefab does not contain an Slot Component");
                    return null;
                }

                //notify this inventory whenever the new invslot contents change
                bs.CoreSlot.subscribers.Add(SlotChanged);

                //reset positions
                UpdateSlots();

                //tell the inserter where to transfer to
                return bs;
            }
            return null;
        }

    }
}
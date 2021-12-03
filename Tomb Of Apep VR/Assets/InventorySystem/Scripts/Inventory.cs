using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GVRI
{
    public class Inventory : MonoBehaviour
    {
        public core.Inventory coreInv;
        public List<Slot> slots;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (coreInv == null) coreInv = new core.Inventory();
            if (slots == null) slots = new List<Slot>();

            //get all child objects with Slot component
            UpdateSlots();
            Close(); //to prevent the inventory staying open at startup
        }

        public virtual Slot FindSlotWithItem(core.ItemInfo itemInfo)
        {
            foreach (Slot s in slots)
            {
                if (s.CoreSlot.ItemInfo.Equals(itemInfo))
                {
                    return s;
                }
            }
            return null;
        }

        public virtual Slot FindSlotWithSlot(core.Slot slot)
        {
            foreach (Slot s in slots)
            {
                if (s.CoreSlot.Equals(slot))
                {
                    return s;
                }
            }
            return null;
        }

        public virtual void UpdateSlots()
        {
            //get all child objects with inventorySlot component
            slots = new List<Slot>(GetComponentsInChildren<Slot>());
            foreach(Slot s in slots)
            {
                coreInv.slots.Add(s.CoreSlot);
            }
        }

        public virtual void Open()
        {
            foreach (Slot s in slots)
            {
                s.Open();
            }
        }

        public virtual void Close()
        {
            foreach (Slot s in slots)
            {
                s.Close();
            }
        }
    }
}
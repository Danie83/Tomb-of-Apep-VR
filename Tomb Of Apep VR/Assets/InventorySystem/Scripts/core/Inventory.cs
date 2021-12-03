using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI.core
{
    public class Inventory
    {
        public HashSet<Slot> slots = new HashSet<Slot>();

        //looks through all inventorySlots the Inventory has for one containing a specified ItemInfo
        //and returns that Inventory Slot (null if none was found)
        public virtual Slot FindSlotWithItem(ItemInfo itemInfo)
        {
            foreach (Slot slot in slots)
            {
                if (slot.ItemInfo.Equals(itemInfo))
                {
                    return slot;
                }
            }
            return null;
        }

    }
    
}
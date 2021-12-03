using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GVRI.core{
    [Serializable]
    public class Slot
    {
        //send message whenever itemcount is changed
        public delegate void notifyNewItemCount(Slot slot);
        public List<notifyNewItemCount> subscribers = new List<notifyNewItemCount>();

        [SerializeField]
        private ItemInfo _itemInfo;
        public ItemInfo ItemInfo {
            get { 
                //if(_itemInfo == null) Debug.Log("get is null "); 
                return _itemInfo; 
            }
            set {
                if (_itemInfo == value) //no changes
                    return;
                if (value == null) //no value
                {//there is no longer an item that can be associated with count
                    _itemCount = 0;
                }
                else if(_itemInfo == null) //changed from null (add 1)
                {
                    _itemCount = 1;
                }
                _itemInfo = value;

                NotifySubscribers();
            }
        }
        [SerializeField]
        private uint _itemCount;
        public uint ItemCount { 
            get => _itemCount; 
            set {
                if (_itemCount == value) //no changes
                    return;

                _itemCount = value;
                if (_itemCount == 0) //no contents left
                {//there is no item stored in here anymore
                    _itemInfo = null;
                }else if(_itemInfo == null)
                {
                    _itemCount = 0;
                }
                NotifySubscribers();
            } 
        }

        // Start is called before the first frame update
        public Slot()
        {
            if (_itemInfo && ItemCount == 0)
            {//this doesn't make sense
                Debug.LogWarning("There is a stored ItemInfo, but ItemCount is 0. Removing Items");
                _itemInfo = null;
            }
            else if (!_itemInfo && ItemCount > 0)
            {//this doesn't make sense
                Debug.LogWarning("There is a positive ItemCount but no ItemInfo. Removing Items");
                ItemCount = 0;
            }
        }

        //add a <count> ItemInfos to the InventorySlot
        //returns true if succeeded
        public uint Add(ItemInfo itemInfo, uint count)
        {
            if(itemInfo.Equals(_itemInfo) || _itemInfo == null)
            {
                /* For capped items (future)
                if (StoredItemCount + count > maxStorage)
                {
                    uint storeCount = maxStorage - StoredItemCount;
                    StoredItemCount += storeCount;
                    return count - storeCount;
                }
                */
                _itemInfo = itemInfo;
                _itemCount += count;
                NotifySubscribers();
                return count;
            }
            return 0;
        }

        //remove all items
        public void Clear()
        {
            Remove(_itemCount);
        }

        //removes <count> from InventrySlot. returns the number of items removed 
        public uint Remove(uint count)
        {
            if (_itemCount > 0)
            {//can pick an item because there are some stored
                uint itemsRemoved;

                if (_itemCount > count)
                {
                    itemsRemoved = count;
                    _itemCount -= count;
                }
                else
                {//no further items left
                    itemsRemoved = _itemCount;
                    _itemCount = 0;
                    _itemInfo = null;
                }
                NotifySubscribers();
                return itemsRemoved;
            }

            return 0;
        }

        //this function just loops through all delegate Functions and calls them
        void NotifySubscribers()
        {
            //remove all nulls
            subscribers.RemoveAll(item => item == null);
            //notify subscribers
            foreach (notifyNewItemCount n in subscribers)
            {
                n(this);
            }
        }
    }
}
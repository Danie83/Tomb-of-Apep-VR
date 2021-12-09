using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI
{
    public class Inserter : MonoBehaviour
    {

        public delegate Slot notifyIncomingItem(Inserter inserter, Item i);
        public notifyIncomingItem target = null;

        private void OnTriggerStay(Collider other)
        {

            Item item = other.GetComponent<Item>();
            if (!item)
            {//its not an item
                return;
            }

            GameObject go = item.gameObject;
            if (go.transform.parent != null)
            {//this item is attached to something, so insertion was probably not intended
                return;
            }

            Slot slot = target?.Invoke(this, item);
            if (slot) slot.Store(go);
        }
    }
}
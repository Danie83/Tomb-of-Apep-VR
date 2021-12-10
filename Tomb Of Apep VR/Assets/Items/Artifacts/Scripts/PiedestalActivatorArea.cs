using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GVRI;

public class PiedestalActivatorArea : MonoBehaviour
{
    public bool isActive = false;
    public GVRI.core.ItemInfo activationItemInfo;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        bool valid = item != null && activationItemInfo != null;
        if (valid && item.itemInfo.name.Equals(activationItemInfo.name))
            isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        bool valid = item != null && activationItemInfo != null;
        if (valid && item.itemInfo.name.Equals(activationItemInfo.name))
            isActive = false;
    }
}

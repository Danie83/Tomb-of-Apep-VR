using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GVRI;
using System;

public class Distributor : MonoBehaviour
{
    public Inserter inserter;
    [Serializable]
    public struct Target
    {
        public GVRI.core.ItemInfo itemInfo;
        public Slot slot;
    }
    public Target[] targets;

    // Start is called before the first frame update
    void Start()
    {
        if (!inserter)
        {//the inserter is the point where items will get put into
            Debug.LogError("[Distributer] Kind of pointless without an Inserter");
        }
        else
        {
            inserter.target = InsertionIntoInserter;
        }
    }

    public Slot InsertionIntoInserter(Inserter inserter, Item i)
    {
        foreach(Target t in targets)
        {
            if(t.itemInfo == i.itemInfo)
            {
                return t.slot;
            }
        }
        return null;
    }
}

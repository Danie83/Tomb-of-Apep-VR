using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System;

namespace GVRI
{
    [CustomEditor(typeof(Slot))]
    public class SlotEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Label("Core-Slot");

            Slot s = (Slot)target;
            core.ItemInfo newItemInfo = (core.ItemInfo)EditorGUILayout.ObjectField("Item Info", s.CoreSlot.ItemInfo, typeof(core.ItemInfo), false);
            uint newItemCount = (uint)EditorGUILayout.IntField("Item Count", (int)s.CoreSlot.ItemCount);

            if (GUI.changed)
            {
                if(newItemInfo != s.CoreSlot.ItemInfo) //changed
                    s.CoreSlot.ItemInfo = newItemInfo;
                else if (newItemCount != s.CoreSlot.ItemCount) //changed, and it has not been changed by the change of the item info (which adds 1)
                    s.CoreSlot.ItemCount = newItemCount;
                EditorUtility.SetDirty(target);
            }
        }
    }
}
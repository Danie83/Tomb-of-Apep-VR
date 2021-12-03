using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace GVRI
{
    public class SimpleInventory : Inventory
    {
        public Hand hand;
        public bool copy_orientation = true;
        public bool toggleable = false;

        protected override void Start()
        {
            base.Start();
            if (!hand) Debug.LogWarning("[QuickAccessInventory] Need a hand to read input");
        }

        protected bool lastFrameValue = false;
        protected virtual void Update()
        {
            if (!hand) return; //need a hand

            bool value = false;
            if (hand.device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out value))
            {
                Input(value);
            }
            lastFrameValue = value;
        }

        void SetOrientation()
        {
            var target = hand.gameObject.transform;
            transform.position = target.position;
            transform.rotation = target.rotation;
        }

        bool isOpen = false;
        protected virtual void Input(bool value)
        {

            if (toggleable)
            {
                if (!isOpen && value && !lastFrameValue)
                {// player started press
                    SetOrientation();
                    Open();
                }

                if (!value && lastFrameValue)
                {// player stopped press
                    if (!isOpen) isOpen = true;
                    else
                    {
                        isOpen = false;
                        Close();
                    }
                }
            }
            else
            {
                if (value && !lastFrameValue)
                {// player started press
                    SetOrientation();
                    Open();
                }

                if (!value && lastFrameValue)
                {// player stopped press
                    Close();
                }
            }
        }
    }
}
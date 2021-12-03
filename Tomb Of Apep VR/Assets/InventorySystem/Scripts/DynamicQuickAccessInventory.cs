using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEditor;

namespace GVRI{
    public class DynamicQuickAccessInventory : DynamicInventory
    {

        public Hand hand;
        public float radius = 1.0f;
        public float separation = 0.3f;

        private void OnDrawGizmosSelected()
        {
            if (!inserter) return;

            var count = maxSlots;
            if (count == 0) return;

            //rotate the inventory slots around the inserter
            float deltaPhi = separation / radius;
            float startRad = -deltaPhi * (count - 1) / 2.0f; //-1 because of the area in between and -1 because the inserter doesn't count
            for (int i = 0; i < count; i++)
            {
                float phi = startRad + deltaPhi * i;
                //place relative to insert coordinates
                Vector3 point = new Vector3(Mathf.Sin(phi) * radius, 0, Mathf.Cos(phi) * radius);
                Gizmos.DrawWireSphere(inserter.transform.TransformPoint(point), 0.01f); //make it a point
            }
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Close(); //to prevent the inventory staying open at startup

            if (!hand) Debug.LogError("[DynamicQuickAccessInventory] Need a hand to read input");

        }

        bool lastFrameValue = false;
        void Update()
        {
            if (!hand) return; //need a hand

            bool value = false;
            if (hand.device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out value))
            {
                Input(value);
            }
            lastFrameValue = value;
        }

        void Input(bool value)
        {
            if (value && !lastFrameValue)
            {//just opened
                Open();
            }

            if (!value && lastFrameValue)
            {//just closed
                Close();
            }
        }

        public override void Open()
        {
            base.Open();
            if (inserter) inserter.gameObject.SetActive(true);
        }

        public override void Close()
        {
            base.Close();
            if (inserter) inserter.gameObject.SetActive(false);
        }

        public override void UpdateSlots()
        {
            base.UpdateSlots();

            if (!inserter) return;

            var count = coreInv.slots.Count;
            if (count == 0) return;

            //rotate the inventory slots around the inserter
            float deltaPhi = separation / radius;
            float startRad = -deltaPhi * (count - 1) / 2.0f; //-1 because of the area in between
            int i = 0;
            foreach (Slot bs in slots)
            {
                float phi = startRad + deltaPhi * i;
                //place relative to insert coordinates
                bs.gameObject.transform.parent = null;
                bs.gameObject.transform.position = inserter.transform.TransformPoint(
                    Mathf.Sin(phi) * radius,
                    0,
                    Mathf.Cos(phi) * radius
                );
                //make the inventory slot look towards the center of the iteminserter
                bs.gameObject.transform.LookAt(inserter.transform.position, inserter.transform.up);
                bs.gameObject.transform.parent = transform; //set parent pack to inventory
                i++;
            }

        }
    }
}
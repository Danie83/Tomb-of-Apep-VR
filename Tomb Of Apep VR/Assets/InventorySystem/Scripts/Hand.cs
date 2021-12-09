using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace GVRI{
    public class Hand : MonoBehaviour
    {
        public XRNode node;
        public InputDevice device;
        public ActionBasedController input;

        public GameObject gameObjectInHand = null;

        bool justPressed = false;
        bool lastTriggerState = false;

        Slot slot = null;

        Rigidbody rb = null;
        Vector3 oldPos;
        Vector3 velocity = new Vector3(0,0,0);

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            oldPos = transform.position;
        }

        //its important that it is a fixed update. Update is called multiple times so "justPressed" will not always work
        void FixedUpdate()
        {
            //velocity, needed for thowing
            Vector3 newPos = transform.position;
            var delta = (newPos - oldPos);
            velocity = delta / Time.deltaTime;
            oldPos = newPos;

/*            if (!device.isValid)
            {//device cannot give input
                device = InputDevices.GetDeviceAtXRNode(node);
                if (!device.isValid) return; //this happens if for example the controller is turned off
            }*/

            bool triggerValue =  input.selectAction.action.ReadValue<float>() > 0;
            Debug.Log(triggerValue);
            //was able to get input from device, so sending the result to input function to handle
            Input(triggerValue);

            
            lastTriggerState = triggerValue;

        
        }
    
        //this is extracted for cleaner input handling
        void Input(bool triggerValue)
        {
            if (!lastTriggerState && triggerValue)
            {//just pressed
                justPressed = true;
                if (slot)
                {//a valid inventorySlot
                    if (!gameObjectInHand)
                    {//holding no gameObjects
                        core.ItemInfo itemInfo = slot.CoreSlot.ItemInfo;
                        uint itemsRemoved = slot.CoreSlot.Remove(1);
                        if (itemsRemoved > 0)
                        {//sucessfully pulled an Item from InventorySlot
                            AttachGameObject(
                                Instantiate(itemInfo.prefab, transform.position, transform.rotation)
                            );
                        }
                    }

                }
            }
            else
            {
                justPressed = false;
            }

            if (lastTriggerState && !triggerValue)
            {//just released
                if (gameObjectInHand)
                {//holding a gameObject that could potentially be an item to store.
                    DropGameObjectInHand();
                }
            }
        }

        void DropGameObjectInHand()
        {
            gameObjectInHand.transform.parent = null;
            //reenable physics
            Rigidbody go_rb = gameObjectInHand.GetComponent<Rigidbody>();
            if (go_rb)
            {
                go_rb.constraints = RigidbodyConstraints.None;

                go_rb.isKinematic = false;
                //apply throw
                go_rb.velocity = velocity;

                
            }        

            gameObjectInHand = null;
        }

        void AttachGameObject(GameObject go)
        {
            go.transform.parent = gameObject.transform;
            gameObjectInHand = go;
            //disable physics for the time being
            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb)
                rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void OnTriggerEnter(Collider other)
        {
            Slot othersInvSlot = other.GetComponent<Slot>();
        
            if (othersInvSlot) {
                //Debug.Log("inventory Slot found");
                slot = othersInvSlot;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Item item = other.GetComponent<Item>();
            if (item && !gameObjectInHand)
            {//its an item and there is none in this hand
                if (justPressed)
                {
                    if (item.transform.parent != null)
                    {
                        Hand otherHand = item.transform.parent.gameObject.GetComponent<Hand>();
                        if (otherHand)
                        {//its another hand, swap the item between hands
                            otherHand.gameObjectInHand = null;
                        }
                        else
                        {
                            //its some kind of different GameObject (could be an apple on a tree or a berry in a bush)
                        }
                    }
                    else
                    {
                        //item is not attached to anything
                    }
                    AttachGameObject(other.gameObject);
                } 
            }
        }

        private void OnTriggerExit(Collider other)
        {

            Slot othersInvSlot = other.GetComponent<Slot>();
            if (othersInvSlot)
            {
                //Debug.Log("inventory Slot no more");
                slot = null;
            }
        }
    }
}
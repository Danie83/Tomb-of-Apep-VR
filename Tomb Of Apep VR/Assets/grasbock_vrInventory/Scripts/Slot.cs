using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GVRI{

    public class Slot : MonoBehaviour
    {

        ~Slot()
        {
            _coreSlot.subscribers.Remove(NotifySlotChange);
        }

        void Awake()
        {
            if (_coreSlot == null)
                CoreSlot = new core.Slot();
            _coreSlot.subscribers.Add(NotifySlotChange);
        }

        //the thing containing the data
        [SerializeField, HideInInspector]
        private core.Slot _coreSlot = new core.Slot();
        public core.Slot CoreSlot
        {
            get => _coreSlot;
            set
            {
                if(_coreSlot != null)
                    _coreSlot.subscribers.Remove(NotifySlotChange);
                _coreSlot = value;
                if (_coreSlot != null)
                {
                    _coreSlot.subscribers.Add(NotifySlotChange);
                    NotifySlotChange(_coreSlot);
                }
                
            }
        }

        //public float distance = 0.1f;
        public float pushForce = 1.0f;
        (GameObject, core.ItemInfo) preview;
        public float previewRotationSpeed = 1.0f;
        public float previewSizeFactor = 0.8f;
        // Start is called before the first frame update
        void Start()
        {
            NotifySlotChange(_coreSlot);
            if (CoreSlot.ItemInfo != null && preview.Item1 == null)
            {//make a preview of the item in storage if there were none before
                //should only contain graphical elements
                preview.Item1 = CreatePreviewCopy(CoreSlot.ItemInfo.prefab);

            }
        }

        private void NotifySlotChange(core.Slot s)
        {
            core.ItemInfo ii = s.ItemInfo;
            if (preview.Item2 != ii)
            {
                Destroy(preview.Item1);
                preview.Item2 = ii;
                if (ii)
                {
                    //make a preview of the item in storage if there were none before
                    //should only contain graphical elements
                    preview.Item1 = CreatePreviewCopy(ii.prefab);
                }
            }
        }

        void Update()
        {//slowly rotate itemPreview around
            if (preview.Item1)
            {
                preview.Item1.transform.LookAt(
                    preview.Item1.transform.position + Vector3.up,
                    new Vector3(Mathf.Cos(Time.time), 0.0f, Mathf.Sin(Time.time))
                );
            }
        }

        //Creates a preview of the GameObject specified in the ItemInfo
        GameObject CreatePreviewCopy(GameObject original)
        {
            GameObject preview = Instantiate(original, transform.position, transform.rotation);

            //remove all components that are not used for visuals
            var components = preview.GetComponents<Component>();
            foreach(Component comp in components)
            {
                if ( !(comp is MeshRenderer) &&
                    !(comp is MeshFilter) &&
                    !(comp is Transform)
                )
                {
                    Destroy(comp);
                } 
            }

            //set parent
            preview.transform.parent = transform;

            //resize a little
            preview.transform.localScale *= previewSizeFactor;

            return preview;
        }

        //Attempt to store a GameObject and returns true if succeeded
        public bool Store(GameObject go)
        {
            if (!go) return false; //gameobeject does not exist

            Item item = go.GetComponent<Item>();
            if (!item)
            {//not an item, don't care
                return false;
            }

            if (go.transform.parent != null)
            { //this item is being held, insertion probably not intended
                return false;
            }

            core.ItemInfo itemInfo = item.itemInfo;
            if (CoreSlot.Add(itemInfo, 1) == 1)
            {//sucessfully moved object into inventorySlot
                //remove the item with gameobject
                Destroy(go);
                go.SetActive(false); //disable anything till deleted (OnTriggerStay gets called multiple times before OnDestroy)
                return true;
            }
            //push the item out of the inventory
            item.push((go.transform.position - transform.position) * pushForce);
            return false;
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            Store(other.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            Store(other.gameObject);
        }
    }
}
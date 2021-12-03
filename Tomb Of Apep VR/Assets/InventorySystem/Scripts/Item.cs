using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI{
    public class Item : MonoBehaviour
    {
        public core.ItemInfo itemInfo = null;

        //rigidbody
        Rigidbody rb = null;
        // Start is called before the first frame update
        void Start()
        {
            if (!rb) rb = gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void push(Vector3 force)
        {
            if (!rb) rb = gameObject.AddComponent<Rigidbody>();
            rb.AddForce(force);
        }

        void OnDestroy()
        {
            //Debug.Log("OnDestroy");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI { 
    public class ItemCountUI : MonoBehaviour
    {
        TextMesh tm = null;
        MeshRenderer mr = null;
        public Slot slot;
        public GameObject target;

        public Transform origin;
        public float circleFitRadius = 1.0f;

        ~ItemCountUI()
        {
            if (slot != null)
            {
                slot.CoreSlot.subscribers.Remove(CountChanged);
            }
        }

        void OnDrawGizmosSelected()
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(
                transform.position,
                transform.lossyScale.z * circleFitRadius
            );
        }

        public void CountChanged(core.Slot s)
        {
            //do not display 1 and 0
            if (s.ItemCount < 2) gameObject.SetActive(false);
            else gameObject.SetActive(true);

            //show count
            tm.text = s.ItemCount.ToString();

        
            //set to correct character size (more numbers need more space), so that they fit into a circle
            Vector2 span = TextMeshSpan();
            tm.gameObject.transform.localScale *= 2.0f * transform.lossyScale.z * circleFitRadius / Mathf.Sqrt(span.y * span.y + span.x * span.x);       
        }

        Vector2 TextMeshSpan()
        {//this returns the span of the textMesh (in world space)
            //this is a workaround to get the textMesh bounds independent of rotation
            Quaternion rotation = tm.gameObject.transform.rotation;
            tm.gameObject.transform.rotation = new Quaternion();
            //now that there are no rotations, one can safely get the bounds
            Vector2 ret = new Vector2(mr.bounds.size.x, mr.bounds.size.y);
            //of course do not forget to reset the rotation
            tm.gameObject.transform.rotation = rotation;
            return ret;
        }

        // Start is called before the first frame update
        void Start()
        {
            tm = GetComponentInChildren<TextMesh>();
            if (!tm) tm = gameObject.AddComponent<TextMesh>();

            mr = tm.gameObject.GetComponent<MeshRenderer>();
            if (!mr) mr = tm.gameObject.AddComponent<MeshRenderer>();

            if (!target)
            {//since no target has been specified I will assume you want the numbers to show towards the camera
                target = GameObject.Find("Camera");
            }

            if (!slot) slot = gameObject.GetComponentInParent<Slot>();
            if (!slot) Debug.LogWarning("No InventorySlot could be found to display the Count");
            else
            {
                slot.CoreSlot.subscribers.Add(CountChanged);
                CountChanged(slot.CoreSlot); //first update
            }

            if (!origin) origin = transform; //if none has been specified, take your own
        }

        // Update is called once per frame
        void Update()
        {
            if (!slot) return;
            //reposition
            if (target)
            {//there is a target to face towards
                origin.LookAt(target.transform);
            }
        
        }
    }
}
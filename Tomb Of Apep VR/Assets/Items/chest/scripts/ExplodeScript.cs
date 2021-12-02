using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public float minForce;
    public float maxForce;
    public float radius;
    public float destroyDelay;



    public void Explode()
    {
            foreach (Transform t in transform)
            {
                var rb = t.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = true;
                    rb.isKinematic = false;
                    rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
                }
                Destroy(t.gameObject, destroyDelay);
            }
    }
}

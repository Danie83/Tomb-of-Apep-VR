using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEnemy : MonoBehaviour
{
    public LayerMask LayerMask;
    public Animator animator;
    public bool prepared = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.forward, out hit, 0.5f, LayerMask))
        {
            Debug.Log("Found an object - distance: " + hit.distance + " " + hit.transform.gameObject.ToString());
            if (prepared) {
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", true);
                prepared = false;
            }
        }
        else
        {
            prepared = true;
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsAttacking", false);
        }



            
    }

}

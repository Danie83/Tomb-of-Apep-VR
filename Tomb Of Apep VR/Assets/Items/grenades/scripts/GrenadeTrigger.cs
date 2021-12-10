using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GrenadeTrigger : MonoBehaviour
{
    public int timer = 3;
    private bool boom = false;
    float countdown;
    private bool alreadyExploded = false;
    public GameObject particleSystem;
                             
    private void Start()
    {
        countdown = timer;
    }
    private void Update()
    {
        if (transform.childCount < 3)
            boom = true;
        if (boom)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f  && !alreadyExploded)
            {
                Explode();
                alreadyExploded = true;
                
            }
        }
    }
    protected virtual void Explode()
    {
        Instantiate(particleSystem, transform);
    }
}

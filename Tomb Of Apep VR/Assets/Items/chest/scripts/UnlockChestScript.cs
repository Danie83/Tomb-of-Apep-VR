using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockChestScript : MonoBehaviour
{
    public AudioSource unlocked;
    public float volume = 0.5f;
    void OnTriggerEnter(Collider col)
    {
        unlocked.PlayOneShot(unlocked.clip, volume);
        transform.parent.GetComponent<OpenChestScript>().locked = false;
        transform.parent.GetComponent<OpenChestScript>().DestroyObj();
        
    }
    void OnTriggerStay(Collider col)
    {

    }
    void OnTriggerExit(Collider col)
    {

    }
}

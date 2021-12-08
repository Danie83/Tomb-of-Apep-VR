using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballInBarrel : MonoBehaviour
{
    public Text textComp;

    void OnCollisionEnter(Collision collision)
    {
        textComp.text = "You won!";
        GetComponent<Collider>().enabled = false;
    }
}

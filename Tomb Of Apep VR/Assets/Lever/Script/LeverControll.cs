using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverControll : MonoBehaviour
{
    [SerializeField]
    private float forwardBackwardTilt = 0f;

    [SerializeField]
    private bool isActivated = false;
    public bool IsActivated { get => isActivated; }


    // Update is called once per frame
    void Update()
    {
        forwardBackwardTilt = Mathf.Min(transform.localEulerAngles.x);
    }

    private float ClampAngle(float x)
    {
        if (x > 180)
            x = x - 360;
        x = Mathf.Clamp(x, -45, 45);
        if (x > 40f && x <= 45)
        {
            isActivated = true;
            x = 45;
        }
        else if (x < -40 && x > -45)
        {
            isActivated = false;
            x = -45;
        }
        return x;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            transform.LookAt(other.transform.position, transform.up);
            // Make sure enemy's rotation is clamped between -45 and 45 degrees
            transform.localEulerAngles = new Vector3
            (
                 ClampAngle(transform.localEulerAngles.x),0,0
            );
        }
    }


}

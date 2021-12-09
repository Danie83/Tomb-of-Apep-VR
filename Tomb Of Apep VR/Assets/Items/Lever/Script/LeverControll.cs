using UnityEngine;
using UnityEngine.InputSystem;

public class LeverControll : MonoBehaviour
{
    [SerializeField]
    private float forwardBackwardTilt = 0f;

    [SerializeField]
    InputActionProperty leftGrabHandle;
    [SerializeField]
    InputActionProperty rightGrabHandle;

    [SerializeField]
    private bool isActivated = false;

    private bool isGrabing = false;
    public bool IsActivated { get => isActivated; }

    private void Start()
    {
        leftGrabHandle.action.started += (context) => { isGrabing = true; };
        leftGrabHandle.action.canceled += (context) => { isGrabing = false; };
        rightGrabHandle.action.started += (context) => { isGrabing = true; };
        rightGrabHandle.action.canceled += (context) => { isGrabing = false; };
    }

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
        if (other.CompareTag("PlayerHand") && isGrabing)
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

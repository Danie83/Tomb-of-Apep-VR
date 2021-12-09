using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private string isCloseAnimParam = "IsClose";
    private Animator doorAnimator;
    public bool isClosed = true;
    private bool lastClosedState = true;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (lastClosedState != isClosed)
        {
            if (isClosed)
            {
                CloseDoor();
            } else
            {
                OpenDoor();
            }
        }
    }
    
    public void CloseDoor()
    {
        doorAnimator.SetBool(isCloseAnimParam, true);
        isClosed = true;
        lastClosedState = isClosed;
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool(isCloseAnimParam, false);
        isClosed = false;
        lastClosedState = isClosed;
    }
}

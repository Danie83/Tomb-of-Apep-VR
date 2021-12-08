using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private string isCloseAnimParam = "IsClose";
    private Animator doorAnimator;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
    }

    public void CloseDoor()
    {
        doorAnimator.SetBool(isCloseAnimParam, true);
    }

    public void OpenDoor()
    {
        doorAnimator.SetBool(isCloseAnimParam, false);
    }
}

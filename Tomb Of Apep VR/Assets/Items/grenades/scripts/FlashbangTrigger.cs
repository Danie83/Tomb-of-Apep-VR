using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbangTrigger : GrenadeTrigger
{
    protected override void Explode()
    {
        base.Explode();
        Debug.Log("FlashBang");
    }

}

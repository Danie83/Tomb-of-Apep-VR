using UnityEngine;

public class FlashbangTrigger : GrenadeTrigger
{
    protected override void Explode()
    {
        base.Explode();
        Debug.Log("FlashBang");
    }

}

using UnityEngine;

public class MK2Trigger : GrenadeTrigger   
{
    public override void DestroyGrenade()
    {
        Destroy(base.Particle_System, 1);
        Destroy(gameObject, 1f);
    }

    protected override void Explode()
    {
        base.Explode();
        Debug.Log("Mk2Grenade");
    }
}

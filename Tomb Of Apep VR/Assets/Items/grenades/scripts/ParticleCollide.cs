using System;
using UnityEngine;

public class ParticleCollide : MonoBehaviour
{
    public int particleDamage;
    private void OnParticleCollision(GameObject other)
    {
        try
        {
            Player player = GetComponentInParent<Player>();
            if (player != null)
            {
                player.TakeDamage(particleDamage);
            }
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}

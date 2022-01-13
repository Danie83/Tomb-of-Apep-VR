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
            } else if (tag.Equals("enemy")) {
                EnemyWalk enemy = GetComponent<EnemyWalk>();
                enemy.SetStun(true);
            }
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}

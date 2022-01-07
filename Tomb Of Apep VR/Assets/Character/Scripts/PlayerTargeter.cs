using UnityEngine;

public class PlayerTargeter : MonoBehaviour
{
    [SerializeField]
    private EnemyWalk enemyWalk;

    void Update()
    {
        if (enemyWalk != null)
            enemyWalk.MoveToLocation(transform.position);        
    }
}

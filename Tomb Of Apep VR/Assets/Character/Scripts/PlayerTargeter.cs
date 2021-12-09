using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeter : MonoBehaviour
{
    [SerializeField]
    private EnemyWalk enemyWalk;

    void Update()
    {
        enemyWalk.MoveToLocation(transform.position);        
    }
}

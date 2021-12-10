using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyWalk : MonoBehaviour
{
    [SerializeField]
    private int dificulty = 1;
    [SerializeField]
    private bool isStunned = false;
    private NavMeshAgent agent;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        switch (dificulty)
        {
            case 1:
                agent.acceleration = 20;
                agent.speed = 1;
                agent.angularSpeed = 300;
                break;
            case 2:
                agent.acceleration = 20;
                agent.speed = 2;
                agent.angularSpeed = 300;
                break;
            case 3:
                agent.acceleration = 20;
                agent.speed = 2.5f;
                agent.angularSpeed = 300;
                break;
            default:
                agent.acceleration = 20;
                agent.speed = 1;
                agent.angularSpeed = 300;
                break;
        }

        animator.SetBool("IsStunned", isStunned);
        animator.SetBool("IsRunning", !isStunned);
    }

    public void MoveToLocation(Vector3 targetPoint)
    {
        if (!isStunned)
        {
            agent.destination = targetPoint;
            agent.isStopped = false;
        }
    }

    public void SetStun(bool stun)
    {
        isStunned = stun;
        animator.SetBool("IsStunned", stun);
        animator.SetBool("IsRunning", !stun);
    }
}
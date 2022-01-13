using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyWalk : MonoBehaviour
{
    [SerializeField]
    private int dificulty = 1;
    [SerializeField]
    private bool isStunned = false;
    [SerializeField]
    private GameObject target;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        target = Camera.main.gameObject;
    }

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (!agent.isOnNavMesh)
            return;
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
    }
    public void Update()
    {
        if (!agent.isOnNavMesh)
            return;
        MoveToLocation(target.transform.position);
    }

    public void MoveToLocation(Vector3 targetPoint)
    {
        if (!isStunned)
        {
            agent.destination = targetPoint;
            agent.isStopped = false;
        } else
        {
            agent.isStopped = true;
        }
    }

    public void SetStun(bool stun)
    {
        isStunned = stun;
        UpdateAnim("IsRunning", !isStunned);
    }

    public void UpdateAnim(string who, bool value)
    {
        animator.SetBool(who, value);
    }
}
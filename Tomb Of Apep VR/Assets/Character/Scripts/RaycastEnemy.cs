using UnityEngine;

public class RaycastEnemy : MonoBehaviour
{
    public LayerMask LayerMask;
    public Animator animator;
    public bool prepared = true;
    private float damageCountdown = 1;

    private EnemyWalk enemy;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<EnemyWalk>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + transform.up, 0.8f, transform.forward, out hit, 0.4f, LayerMask))
        {
            if (prepared) {
                Debug.Log("Found an object - distance: " + hit.distance + " " + hit.transform.gameObject.ToString());
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", true);
                enemy.SetJustStun(true);
                prepared = false;
            }
            Player player = hit.transform.gameObject.GetComponent<Player>();
            damageCountdown -= Time.deltaTime;
            if (damageCountdown <= 0)
            {
                player.TakeDamage(40);
                damageCountdown = 5;
            }
            
        }
        else
        {
            damageCountdown = 1;
            prepared = true;
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsAttacking", false);
            enemy.SetJustStun(false);
        }

    }

}

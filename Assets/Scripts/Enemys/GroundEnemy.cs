using UnityEngine;

public class GroundEnemy : Enemy
{
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected float groundCheckDistance = 0.4f;
    [SerializeField] protected float detectionRadius;
    [SerializeField] protected float attackDistance;

    protected bool isGrounded;
    protected bool playerInRange = false;
    protected Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    protected virtual void Update()
    {
        if (playerPosition != null)
        {
            playerInRange = IsPlayerInRange();
            isGrounded = CheckIfGrounded();

            if (playerInRange && isGrounded)
            {
                FindPlayer();
                if (!IsPlayerInAttackRange())
                {
                    MoveTowardsPlayer();
                }
            }
            else if (isGrounded)
            {
                Patrol();
            }
        }
    }

    protected void FindPlayer()
    {
        playerPosition = Singletone.Instance.transform.position;
    }

    protected bool CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    protected bool IsPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<Singletone>(out Singletone component))
            {
                return true;
            }
        }
        return false;
    }

    protected bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, playerPosition) <= attackDistance;
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector3 direction = (playerPosition - transform.position).normalized;
        direction.y = 0;
        transform.position += direction * speed * Time.deltaTime;

        RotateTowardsPlayer(direction);
    }

    protected virtual void Patrol()
    {
        Debug.Log("Patrolling...");
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}

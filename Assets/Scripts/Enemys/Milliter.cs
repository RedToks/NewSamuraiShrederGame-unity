using UnityEngine;
using System.Collections;

public class Militer : GroundEnemy
{
    [SerializeField] private float attackCooldown = 2f;
    private bool isAttacking = false;
    private PlayerHealth playerHealth;

    protected override void Start()
    {
        base.Start();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    protected override void Update()
    {
        base.Update();

        if (playerInRange && isGrounded)
        {
            if (IsPlayerInAttackRange())
            {
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else if (isGrounded)
        {
            Patrol();
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("attack");
        animator.SetInteger("state", 0);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    public void DealDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage((int)damage); 
        }
        Debug.Log($"Игрок получил {damage} урона");
    }

    protected override void MoveTowardsPlayer()
    {
        if (!isAttacking)
        {
            base.MoveTowardsPlayer();
            animator.SetInteger("state", 1);
        }
    }
}

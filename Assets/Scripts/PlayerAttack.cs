using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    [SerializeField] private string[] attackAnimations;
    [SerializeField] private float attackMoveDistance = 0.25f;
    [SerializeField] private float attackMoveDuration = 0.1f;
    [SerializeField] private float attackCooldown = 0.2f; 

    private bool isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            string randomAttackAnimation = attackAnimations[Random.Range(0, attackAnimations.Length)];
            StartCoroutine(Attack(randomAttackAnimation));
        }
    }

    private IEnumerator Attack(string attackAnimation)
    {
        animator.Play(attackAnimation);
        StartCoroutine(MoveDuringAttack());

        yield return new WaitForSeconds(attackMoveDuration);

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    private IEnumerator MoveDuringAttack()
    {
        float direction = playerMovement.facingRight ? 1f : -1f;
        float moveDistance = playerJump.IsGrounded ? attackMoveDistance : 0f;

        if (moveDistance > 0f)
        {
            Vector2 startPosition = transform.position;
            Vector2 targetPosition = startPosition + new Vector2(direction * moveDistance, 0f);

            float elapsedTime = 0f;
            while (elapsedTime < attackMoveDuration)
            {
                float t = elapsedTime / attackMoveDuration;
                rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}

using UnityEngine;

public class ChaserEnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform PlayerDetectionRange;
    [SerializeField] private Transform groundCheck;

    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InvokeRepeating(nameof(JumpWithRandomDelay), Random.Range(2f, 4f), Random.Range(2f, 4f));
    }

    private void Update()
    {
        player = Player.Instance.transform;

        if (player != null)
        {
            float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

            if (distanceToPlayer <= detectionRadius)
            {
                // Преследуем игрока
                State = States.Run;
                Vector2 moveDirection = new Vector2(player.position.x - transform.position.x, 0).normalized;
                rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

                if (moveDirection.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (moveDirection.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else
            {
                State = States.Idle;
                rb.velocity = Vector2.zero;
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void JumpWithRandomDelay()
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public enum States
    {
        Idle,
        Run
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }
}

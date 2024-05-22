using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;

    [Header("JumpCollider")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] private float groundCheckRadius = 0.2f;
    private Rigidbody2D _rigidbody;
    public bool IsGrounded { get; set; }
    public bool HasJumped { get; set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheckCollider.position, groundCheckRadius, groundLayer);

        if (IsGrounded && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)))
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        HasJumped = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckCollider.position, groundCheckRadius);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private bool facingRight = true;

    private Rigidbody2D _rigidbody;
    private Animator animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {


        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new Vector2(horizontalInput, 0);
        movement.Normalize();
        _rigidbody.velocity = new Vector2(movement.x * _moveSpeed, _rigidbody.velocity.y);

        animator.SetInteger("state", Mathf.Abs(horizontalInput) > 0 ? 1 : 0);

        if (movement.x < 0 && facingRight)
            FlipPlayer();
        else if (movement.x > 0 && !facingRight)
            FlipPlayer();
    }

    private void FlipPlayer()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform AttackRange;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackCooldown = 1.0f; // �������� ����� ������
    [SerializeField] private float attackDuration = 0.5f; // ������������ �������� �����  
    [SerializeField] private Transform PlayerDetectionRange;
    [SerializeField] private float detectionRadius = 5f; // ������ ����������� ������

    private float nextAttackTime = 0f;
    private float attackStartTime = 0f; // ����� ������ �����
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private bool playerInRange = false; // ���� ��� ������������ ������� ������ � ���� �����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        player = Player.Instance.transform;

        if (player != null)
        {
            float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

            // ���������, ��������� �� ����� � ������� ����������� ����
            if (distanceToPlayer <= detectionRadius)
            {
                // ���� ����� ������ ��� ����� � ���� ����������� ����, �������� �������������
                if (!playerInRange)
                {
                    playerInRange = true;
                }

                // ���� ����� ��������� ��� ������� �����, ���������� ���
                if (distanceToPlayer > attackRange)
                {
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
                    // ���� ����� ��������� � ������� ����� ���� � ������ ���������� ������� � ������� ��������� �����, �������
                    if (Time.time >= nextAttackTime)
                    {
                        rb.velocity = Vector2.zero;
                        StartAttack();
                        nextAttackTime = Time.time + attackCooldown;
                    }
                    else if (Time.time - attackStartTime >= attackDuration)
                    {
                        FinishAttack();
                    }
                }
            }
            else
            {
                // ���� ����� ����� �� ���� ����������� ����, ������������� ������������� � ���������� �����������
                playerInRange = false;
                State = States.Idle;
                ResetEnemyAttackCooldown();
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void StartAttack()
    {
        State = States.Attack;
        attackStartTime = Time.time;
    }

    public void FinishAttack()
    {
        State = States.Run;
    }

    public void ResetEnemyAttackCooldown()
    {
        nextAttackTime = Time.time + attackCooldown;
    }

    // ��������� ����� ��� ��������� ����� ������ � ���� ����� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ���������� ����������� �����, ����� ����� ������ � ���� ����� ����
            ResetEnemyAttackCooldown();
        }
    }

    public enum States
    {
        Idle,
        Run,
        Attack
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

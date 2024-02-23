using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform AttackRange;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private float attackCooldown = 1.0f; // Задержка перед атакой
    [SerializeField] private float attackDuration = 0.5f; // Длительность анимации атаки  
    [SerializeField] private Transform PlayerDetectionRange;
    [SerializeField] private float detectionRadius = 5f; // Радиус обнаружения игрока

    private float nextAttackTime = 0f;
    private float attackStartTime = 0f; // Время начала атаки
    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private bool playerInRange = false; // Флаг для отслеживания наличия игрока в зоне атаки

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

            // Проверяем, находится ли игрок в радиусе обнаружения моба
            if (distanceToPlayer <= detectionRadius)
            {
                // Если игрок только что вошел в зону обнаружения моба, начинаем преследование
                if (!playerInRange)
                {
                    playerInRange = true;
                }

                // Если игрок находится вне радиуса атаки, преследуем его
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
                    // Если игрок находится в радиусе атаки моба и прошло достаточно времени с момента последней атаки, атакуем
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
                // Если игрок вышел из зоны обнаружения моба, останавливаем преследование и сбрасываем перезарядку
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

    // Добавляем метод для обработки входа игрока в зону атаки моба
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Активируем перезарядку атаки, когда игрок входит в зону атаки моба
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

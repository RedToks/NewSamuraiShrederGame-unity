using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    public float checkRadius = 0.5f;
    [SerializeField] private float attackForwardForce = 2.0f;
    [SerializeField] private float attackRange = 0.55f;

    [SerializeField] private int attackDamage = 1;
    public static Player Instance { get; private set; }

    [SerializeField] private Transform AttackPoint;
    [SerializeField] private LayerMask enemyLayer;

    private bool onGround;
    private bool facingRight;
    private bool isAttacking = false;
    public bool isPlayerControlEnabled { get; private set; } = true;

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask GroundLayer;
    private Rigidbody2D rb;
    private Animator anim;
    private SlimeEnemy slimeEnemy;
    private EnemyAI enemyAI;
    private ChaserEnemyAI chaserEnemyAI;

    private HealthBar healthBar;
    public SpriteRenderer Sprite { get; private set; }

    private List<int> attackAnimations = new List<int>();

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }


    private void Start()
    {


        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        slimeEnemy = FindObjectOfType<SlimeEnemy>();
        enemyAI = FindObjectOfType<EnemyAI>();
        healthBar = FindObjectOfType<HealthBar>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < 3; i++)
        {
            attackAnimations.Add(i);
        }
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
        if (!isFlashing)
            StartCoroutine(FlashRed());

        ApplyKnockback(knockbackForce);      
    }

    public void EnablePlayerControl()
    {
        isPlayerControlEnabled = true;
    }

    public void DisablePlayerControl()
    {
        isPlayerControlEnabled = false;
    }

    private void Update()
    {
        if (isPlayerControlEnabled)
        {
            CheckingGround();
            if (onGround) State = States.idle;
            if (Input.GetButton("Horizontal"))
                Run();
            if (Input.GetKeyDown(KeyCode.W) && onGround)
                Jump();
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
                StartCoroutine(AttackSequence());
        }
    }

    private void Run()
    {
        if (onGround) State = States.run;
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector2 movement = new(horizontalInput, 0);

        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        if (movement.x < 0 && !facingRight)
            Flip();
        if (movement.x > 0 && facingRight)
            Flip();
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
    }

    private new void Attack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {           
                Debug.Log($"Нанес {attackDamage} дамага врагу ");
                slimeEnemy.TakeDamage(attackDamage);
                ResetEnemyAttackCooldown();
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;

        ShuffleAttackAnimations();

        int randomAttackAnimation = attackAnimations[0];

        Vector2 attackDirection = facingRight ? Vector2.right : Vector2.left;
        rb.AddForce(attackDirection * attackForwardForce);

        State = (States)(randomAttackAnimation + 2);
        Attack();

        yield return new WaitForSeconds(0.3f);

        State = States.idle;
        isAttacking = false;
    }

    private void ShuffleAttackAnimations()
    {
        for (int i = attackAnimations.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = attackAnimations[i];
            attackAnimations[i] = attackAnimations[j];
            attackAnimations[j] = temp;
        }
    }

    private void ResetEnemyAttackCooldown()
    {
        if (enemyAI != null)
        {
            enemyAI.ResetEnemyAttackCooldown();
        }
    }

    private void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, GroundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundCheck.position, checkRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    public enum States
    {
        idle,
        run,
        Attack_01, Attack_02, Attack_03
    }
}

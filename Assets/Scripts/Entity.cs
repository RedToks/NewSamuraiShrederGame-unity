using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] protected float knockbackForce = 2;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private ParticleSystem deathParticles;


    protected bool isFlashing = false;
    private int attackDamage = 1;

    public States State { get; set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Start()
    {
        deathParticles = GetComponent<ParticleSystem>();
        //deathParticles.gameObject.SetActive(false);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;     

        if (currentHealth <= 0)
            Die();
        if (!isFlashing)
            StartCoroutine(FlashRed());

        ApplyKnockback(knockbackForce);
    }

    public virtual IEnumerator FlashRed()
    {
        isFlashing = true;

        if (spriteRenderer != null)
            spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        isFlashing = false;
    }

    public void ApplyKnockback(float knockbackForce)
    {
        Rigidbody2D rb = FindObjectOfType<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDirection = Vector2.right;

            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void RotateTowards(Vector2 targetDirection)
    {
        if (targetDirection.x < 0)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        if (targetDirection.x > 0)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public virtual void Die()
    {
        if (deathParticles != null)
        {
            deathParticles.transform.position = transform.position;
            deathParticles.gameObject.SetActive(true);
            deathParticles.Play();
        }
        Destroy(gameObject);
    }

    public virtual void Attack()
    {
        Player.Instance.TakeDamage(attackDamage);
    }

    public enum States
    {
        Idle,
        Run,
        Attack
    }


}

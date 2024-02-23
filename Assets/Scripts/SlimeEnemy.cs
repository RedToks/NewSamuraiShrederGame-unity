using System.Collections;
using UnityEngine;

public class SlimeEnemy : Entity
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageInterval = 2f;

    private float timeSinceLastDamage = 0f;
    private bool isCollidingWithPlayer = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            isCollidingWithPlayer = true;
            Player.Instance.TakeDamage(damage);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            isCollidingWithPlayer = false;
            timeSinceLastDamage = 0f;
        }
    }

    private void Update()
    {
        if (isCollidingWithPlayer)
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= damageInterval)
            {
                Player.Instance.TakeDamage(damage);
                timeSinceLastDamage = 0f;
            }
        }
    }
}

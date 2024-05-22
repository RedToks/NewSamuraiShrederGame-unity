using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;

    protected Vector3 playerPosition;
    protected virtual void Start()
    {
        playerPosition = Singletone.Instance.playerPosition;
    }


    protected void RotateTowardsPlayer(Vector3 direction)
    {
        if (direction.x >= 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}

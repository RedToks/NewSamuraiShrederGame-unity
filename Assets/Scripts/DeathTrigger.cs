using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private bool isColliding = false;
    private float collisionTime = 0f;
    private Collider2D collisionCollider;

    private void Update()
    {
        if (isColliding)
        {
            collisionTime += Time.deltaTime;
            Debug.Log(collisionTime);
            if (collisionTime >= 5f)
            {
                Player.Instance.Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            Debug.Log("Вход в зону смерти");
            isColliding = true;
            collisionCollider = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player.Instance.gameObject)
        {
            Debug.Log("Выход из зоны смерти");
            isColliding = false;
            collisionTime = 0f;
            collisionCollider = null;
        }
    }
}


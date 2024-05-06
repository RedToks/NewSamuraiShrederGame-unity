using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private string[] attackAnimations;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string randomAttackAnimation = attackAnimations[Random.Range(0, attackAnimations.Length)];

            animator.Play(randomAttackAnimation);
        }
    }
}

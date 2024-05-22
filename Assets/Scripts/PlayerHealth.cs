using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<int> OnHealthChanged;

    [SerializeField] private int _health;

    public int Health
    {
        get { return _health; }
        private set
        {
            _health = value;
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(Health);
        }
    }
}

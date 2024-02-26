using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            ApplyDamage(10);
            Debug.Log("Я нанес 10 урона");
        }
    }
    public void ApplyDamage(int damage)
    {
        Health -= damage;

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(Health);
        }
    }
}

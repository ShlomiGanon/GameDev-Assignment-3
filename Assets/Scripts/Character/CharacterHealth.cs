using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{

    [SerializeField] int fullHealth = 0;
    [SerializeField] private int currentHealth;//[SerializeField] it is for DEBUG

    private void Awake()
    {
        currentHealth = fullHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetFullHealth()
    {
        return fullHealth;
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void ApplyDamage(int damage)
    {
        if (damage <= 0) return;
        if (damage > currentHealth)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }
    }
}

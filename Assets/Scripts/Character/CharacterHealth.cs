using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{

    [SerializeField] CharacterSO data;
    [SerializeField] private int currentHealth;//[SerializeField] it is for DEBUG

    private void Awake()
    {
        currentHealth = data.FullHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetFullHealth()
    {
        return data.FullHealth;
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

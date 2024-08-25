using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxMana = 100f;
    public float currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ConsumeMana(20);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void ConsumeMana(float mana)
    {
        currentMana -= mana;

        manaBar.SetMana(currentMana);
    }
}

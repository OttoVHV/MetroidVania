using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxMana = 100f;
    public float currentMana;

    public int lastSaveroom;
    public bool dashUnlocked;
    public bool wallWalkUnlocked;

    public HealthBar healthBar;
    public ManaBar manaBar;
    public LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    public void Teleport(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ConsumeMana(20);
        }

        if (currentHealth <= 0)
        {
            Die();
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

    public void SetRoom(int room)
    {
        lastSaveroom = room;
    }
    
    public void LoadGame()
    {
        GameData data = SaveSystem.loadGame();

        lastSaveroom = data.lastSaveroom;
        dashUnlocked = data.dashUnlocked;
        wallWalkUnlocked = data.wallWalkUnlocked;

        levelLoader.LoadScene(lastSaveroom);
    }

    private void Die()
    {
        //disable player's control
        //play death animation
        //destroy game object
        //load last savepoint
        LoadGame();
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
}

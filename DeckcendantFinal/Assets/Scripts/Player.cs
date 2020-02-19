using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int currentHealth;
    int maxHealth;
    int energy;
    int block;

    public static Player instance;

    public void TakeDamage(int damage)
    {
        if (block - damage <= 0) currentHealth -= block + damage;
        else block -= damage;
    }

    public void GainBlock(int amount)
    {
        block += amount;
    }

    public void Heal(int amount)
    {
        if (currentHealth + amount > maxHealth) currentHealth = maxHealth;
        else currentHealth += amount;
    }

    public void PlayCard(Crd c) { energy -= c.cost; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

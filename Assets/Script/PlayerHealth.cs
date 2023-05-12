using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        print(currentHealth);
        
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            HealDamage(15);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        print("player damage is now " + currentHealth);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            print("Died!");
        }
    }
    public void HealDamage(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();

    } 
    public void HealthCheck()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
    private void Update()
    {
        print(currentHealth);

        HealthCheck();
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            HealDamage(15);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        //print("player damage is now " + currentHealth);
        
        if (currentHealth <= 0)
        {
            print("Died!");
        }
    }
    public void HealDamage(int healAmount)
    {
        currentHealth += healAmount;
        
    }
    
}

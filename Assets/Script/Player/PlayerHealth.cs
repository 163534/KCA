using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;
    Slider healthBar;
    GameObject healthBarFill;
    public GameObject dMenu;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBarFill = GameObject.Find("HealthBarFill");
        dMenu.SetActive(false);
        
    }
   
    public void HealthBarValue()
    {
        healthBar.value = currentHealth;
    }
    private void Update()
    {
        //print(currentHealth);

        HealthBarValue();
        
      /*  if (Input.GetKeyDown(KeyCode.T))
        {
            HealDamage(15);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TakeDamage(15);
        }*/
        
    }
    public void TakeDamage(int damageAmount)
    {

        AudioManager.instance.PlaySFX("Damaged");
        currentHealth -= damageAmount;

        //print("player damage is now " + currentHealth);
        
        if (currentHealth <= 0)
        {
            //print("Died!");
            healthBarFill.SetActive(false);
            dMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            healthBarFill.SetActive(true);
        }
        
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
    }
    public void HealDamage(int healAmount)
    {
        currentHealth += healAmount;
        
    }
    
}

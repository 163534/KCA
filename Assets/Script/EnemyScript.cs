using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform Player;

    public int enemyHealth, enemyMaxHealth = 60;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        //ph = gameObject.AddComponent<PlayerHealth>();
    }
    private void Update()
    {
        ChasePlayer();
    }
    void EnemyTakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);
    }
    public void OnCollisionEnter(Collision col)
    {
        print("hit " + col.gameObject.tag);
        if (col.collider.tag == "Player")
        {
            print("damaged!");

            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);

        }
    }
    void ChasePlayer()
    {
        nav.SetDestination(Player.position);
    }
}

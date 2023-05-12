using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform PlayerPos;
    public GameObject Player;

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
    public void EnemyTakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);
    }

   

    public void OnCollisionEnter(Collision col)
    {
        print("hit2 " + col.gameObject.tag);
        if (col.collider.tag == "Player")
        {
            print("damaged! pt 1");
            //col.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
            StartCoroutine("Attack");  
            

        }
    }
    void ChasePlayer(bool isChasing)
    {
        if (ChasePlayer(isChasing))
        {
            nav.SetDestination(PlayerPos.position);
        }
    }
    IEnumerator Attack()
    {
        print("coRoutine");
        Player.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
        yield return new WaitForSecondsRealtime(5);
    }
}

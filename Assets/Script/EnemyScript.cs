using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent nav;
    public Transform playerPos;
    public GameObject player;
    public bool chasing;
    float enemySpeed;

    public int enemyHealth, enemyMaxHealth = 60;
    // Start is called before the first frame update
    void Start()
    {
        enemySpeed = 6f;
        player = GameObject.Find("Kyle");
        playerPos = player.transform;
        enemyHealth = enemyMaxHealth;
        chasing = true;
        nav.angularSpeed = 240;
        
    }
    private void Update()
    {
        print(nav.hasPath);
        ChasePlayer();
    }
    public void EnemyTakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);
    }
    public void OnCollisionEnter(Collision col)
    {
        //print("hit2 " + col.gameObject.tag);
        if (col.collider.tag == "Player")
        {
            //print("damaged! pt 1");
            StartCoroutine("Attack");  
        }
    }
    void ChasePlayer()
    {
        if (chasing)
        {
            nav.SetDestination(playerPos.position);
            nav.speed = enemySpeed;
        }
        else
        {
            nav.speed = 0;
        }
    }
    IEnumerator Attack()
    {
       // print("coRoutine");
        player.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
        chasing = false;
        yield return new WaitForSecondsRealtime(3.5f);
        chasing = true;
    }
}

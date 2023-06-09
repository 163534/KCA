using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    NavMeshAgent nav;
    Rigidbody rb;
    public Transform playerPos;
    public GameObject player;
    public bool chasing;
    float enemySpeed;
    int groundCheck;
    public int enemyHealth, enemyMaxHealth = 60;
    void Start()
    {   
        
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
        enemySpeed = 6f;
        player = GameObject.Find("Kyle_Final");
        playerPos = player.transform;
        enemyHealth = enemyMaxHealth;
        chasing = true;
        nav.angularSpeed = 240;

        WhenSpawned();

    }
    private void Update()
    {
        print(nav.hasPath);
        ChasePlayer();
        CheckEnemyHealth();
    }
    public void EnemyTakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);
    }
    public void CheckEnemyHealth()
    {
        if(enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision col)
    {
        //print("hit2 " + col.gameObject.tag);
        if (col.collider.tag == "Player")
        {
            //print("damaged! pt 1");
            StartCoroutine("Attack");  
        }
        if(col.collider.tag == "Floor" && groundCheck == 1)
        {
            nav.enabled = true;
            groundCheck = 0;
        }
    }
    void ChasePlayer()
    {
        if (chasing && nav.enabled == true)
        {
            
            nav.SetDestination(playerPos.position);
            nav.speed = enemySpeed;
        }
        else
        {
            nav.speed = 0;
        }
    }
    void WhenSpawned()
    {
        groundCheck = 1;
        rb.isKinematic = false;
        rb.velocity = (transform.up * 5) + (transform.forward * 6);
    }
    IEnumerator Attack()
    {
       // print("coRoutine");
        player.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        chasing = false;
        yield return new WaitForSecondsRealtime(4.5f);
        chasing = true;
    }
    
}

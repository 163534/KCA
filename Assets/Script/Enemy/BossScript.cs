using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    NavMeshAgent nav;
    public GameObject player;
    public Transform playerPos;
    GameObject summonSpot;
    public Transform summonPos;
    Rigidbody rb;
        
    public Actions actions;
       
    float enemySpeed;
    public bool moveTo;
    
    public int enemyHealth, enemyMaxHealth = 300;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Kyle_Final");
        summonSpot = GameObject.Find("BossSpawner");
        summonPos = summonSpot.transform;

        moveTo = false; 

        if(summonSpot == null)
        {
            print("summonPos not found");
        }
        playerPos = player.transform;

        enemySpeed = 6f;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = enemySpeed;
        nav.angularSpeed = 240;

        enemyHealth = enemyMaxHealth;

        actions = Actions.Attack;
        
    }
    private void Update()
    {
        print(actions);
        
        DoLogic();
        
    }
    void DoLogic()
    {
        CheckEnemyHealth();

        if(actions == Actions.Attack)
        {
            ChasePlayer();
        }
        if(actions == Actions.MoveTo)
        {
            MoveToSummon();
            moveTo = true;
            
        }
        if(actions == Actions.Summon)
        {
            // Summoning gets played from SummoningScript //
        }
    }
    void ChasePlayer()
    {
        nav.SetDestination(playerPos.position);
    }
    void MoveToSummon()
    {
        nav.SetDestination(summonPos.position);
        
    }
    
    public void EnemyTakeDamage(int damageAmount)
    {
        enemyHealth -= damageAmount;
        enemyHealth = Mathf.Clamp(enemyHealth, 0, enemyMaxHealth);
    }
    public void CheckEnemyHealth()
    {
        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
        if(enemyHealth == 240)
        {
            actions = Actions.MoveTo;
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
        
    }
    IEnumerator Attack()
    {
        
        // print("coRoutine");
        player.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        
        yield return new WaitForSecondsRealtime(4.5f);
        
        
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    NavMeshAgent nav;
    Rigidbody rb;
    public Transform playerPos;
    public GameObject sp;
    public Transform summonPos;
  //  [SerializeField] Transform summonPos;
    public GameObject player;
    public Actions actions;

    public GameObject babyTomato;
    public GameObject[] spawnPoint;
    

    public bool chasing;
    float enemySpeed;
    
    public int enemyHealth, enemyMaxHealth = 300;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
        enemySpeed = 6f;
        player = GameObject.Find("Kyle_Final");
        playerPos = player.transform;
        sp = GameObject.Find("SummonPos");
        summonPos = sp.transform;
        enemyHealth = enemyMaxHealth;
        chasing = true;
        nav.angularSpeed = 240;

        actions = Actions.Attack;
        
    }
    private void Update()
    {

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
        }
        if(actions == Actions.Summon)
        {
            SummonTomatos();
        }
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
        if (chasing && nav.enabled == true)
        {

            nav.SetDestination(playerPos.position);
            nav.speed = enemySpeed;
        }
        // sometimes the tomato just falls through the map, I have no idea why but I've added the below code to try and sort it out //
    }
    void MoveToSummon()
    {
        nav.SetDestination(summonPos.position);
    }
    void SummonTomatos()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject obj = Instantiate(babyTomato);
            obj.transform.rotation = spawnPoint[i].transform.rotation;
            obj.transform.position = spawnPoint[i].transform.position;
            if(i == spawnPoint.Length)
            {
                actions = Actions.Attack;
            }
        }
    }
   
    IEnumerator Attack()
    {
        // print("coRoutine");
        player.gameObject.GetComponent<PlayerHealth>().TakeDamage(5);
        chasing = false;
        nav.enabled = false;
        transform.Translate(-transform.forward * enemySpeed * Time.deltaTime);
        yield return new WaitForSecondsRealtime(4.5f);
        chasing = true;
        nav.enabled = true;
    }

}

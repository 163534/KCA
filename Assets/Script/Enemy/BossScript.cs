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
    public Transform summonPos;
    public GameObject player;
    public GameObject summon;

    public bool chasing;
    bool summonCheck;
    bool summonReached;
    float enemySpeed;
    int groundCheck;
    public int enemyHealth, enemyMaxHealth = 300;

    public GameObject babyTomato;
    public GameObject[] spawnPoint;
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Kyle_Final");
        summon = GameObject.Find("BossSpawner");
        playerPos = player.transform;
        summonPos = summon.transform;
        
        enemyHealth = enemyMaxHealth;
        enemySpeed = 6f;
        nav.angularSpeed = 240;

        chasing = true;
        nav.enabled = false;
        summonCheck = false;
        summonReached = false;


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
        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
        if(enemyHealth == 210)
        {
            StartCoroutine("Summoning");
            
        }
        if(enemyHealth == 120)
        {
            StartCoroutine("Summoning");
            
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
        if (transform.position.y < -1)
        {

            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

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
        nav.enabled = false;
        transform.Translate(-transform.forward * enemySpeed * Time.deltaTime);
        yield return new WaitForSecondsRealtime(4.5f);
        chasing = true;
        nav.enabled = true;
    }
    IEnumerator Summoning()
    {
        summonCheck = true;
        if (summonCheck)
        {
            nav.SetDestination(summonPos.position);
            if (summonReached)
            {
                for (int i = 0; i < spawnPoint.Length; i++)
                {
                    GameObject obj = Instantiate(babyTomato);
                    obj.transform.rotation = spawnPoint[i].transform.rotation;
                    obj.transform.position = spawnPoint[i].transform.position;

                }
                yield return new WaitForSecondsRealtime(3);
                summonCheck = false;
            }
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
        if (col.collider.tag == "Floor" && groundCheck == 1)
        {
            nav.enabled = true;
            groundCheck = 0;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Summon"))
        {
            summonReached = true;
        }
    }
}

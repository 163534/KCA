using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningScript : MonoBehaviour
{
    public GameObject babyTomato;
    public GameObject[] spawnPoint;
    BossScript bs;

    int trigger;
    private void Start()
    {
        bs = GameObject.Find("Mother Tomato").GetComponent<BossScript>();
        trigger = 1;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boss") && bs.summonCheck)
        {
            SpawnEnemy();
        }
    }
    public void SpawnEnemy()
    {
        if (trigger == 1)
        {
            for (int i = 0; i < spawnPoint.Length; i++)
            {
                GameObject obj = Instantiate(babyTomato);
                obj.transform.rotation = spawnPoint[i].transform.rotation;
                obj.transform.position = spawnPoint[i].transform.position;

            }
            trigger = 0;
        }
    }
}

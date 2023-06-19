using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject babyTomato;
    public GameObject[] spawnPoint;
    int trigger;
    private void Start()
    {
        trigger = 1;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            SpawnEnemy();
        }
    }
    public void SpawnEnemy()
    {
        if (trigger == 1)
        {
            for(int i = 0; i < spawnPoint.Length; i++)
            {
                GameObject obj = Instantiate(babyTomato);
                obj.transform.rotation = spawnPoint[i].transform.rotation;
                obj.transform.position = spawnPoint[i].transform.position;
                
            }
            trigger = 0;
        }
    }
    
}

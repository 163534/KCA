using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject babyTomato;
    public GameObject spawnPoint;

    private void Start()
    {
        
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
       GameObject obj = Instantiate(babyTomato, spawnPoint.transform);
        obj.transform.rotation = spawnPoint.transform.rotation;
    }
}

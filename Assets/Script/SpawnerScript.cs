using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject babyTomato;
    public GameObject spawnPoint;

    private void Start()
    {
        GameObject.FindObjectsOfTypeAll()
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
        Instantiate(babyTomato, spawnPoint.transform);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject babyTomato;
    public GameObject spawnPoint;
        
    public void SpawnEnemy()
    {
        Instantiate(babyTomato, spawnPoint.transform);
    }
}
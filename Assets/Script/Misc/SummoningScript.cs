using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningScript : MonoBehaviour
{
   [SerializeField] BossScript bs;
    public GameObject motherTomato=null;
    public GameObject babyTomato;
    public GameObject[] spawnPoint;
    int trigger;
    private void Start()
    {
        
        
        
    }

    private void Update()
    {
        if(motherTomato == null)
        {
            motherTomato = GameObject.Find("Mother Tomato");
        }

   



        Summon();
    }
    void Summon()
    {
        if (motherTomato)
        {
            bs = motherTomato.GetComponent<BossScript>();
        }
        if (bs == null)
        {
            return;
        }

        if (bs.actions == Actions.Summon)
        {
            trigger = 1;
            if (trigger == 1)
            {
                for (int i = 0; i < spawnPoint.Length; i++)
                {
                    GameObject obj = Instantiate(babyTomato);
                    obj.transform.rotation = spawnPoint[i].transform.rotation;
                    obj.transform.position = spawnPoint[i].transform.position;

                }
                trigger = 0;
                bs.actions = Actions.Attack;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            
                bs.actions = Actions.Summon;
            
            
        }
    }
}

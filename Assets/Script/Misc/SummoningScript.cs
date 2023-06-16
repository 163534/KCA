using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningScript : MonoBehaviour
{
   [SerializeField] BossScript bs;
    public GameObject motherTomato;
    private void Start()
    {

        bs = motherTomato.GetComponent<BossScript>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boss") && bs.actions == Actions.MoveTo)
        {
            bs.actions = Actions.Summon;
        }
    }
    
}

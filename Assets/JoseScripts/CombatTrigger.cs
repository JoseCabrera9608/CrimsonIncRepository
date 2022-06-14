using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    public int id;
    //public ProgressManager progress;

    private void Start()
    {
        //progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            BossGameEVent.current.StartCombatTriggerExit(id);
            Destroy(this.gameObject);
        }
        

        
        

    }
}

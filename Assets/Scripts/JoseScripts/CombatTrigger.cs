using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{

    //public ProgressManager progress;

    private void Start()
    {
        //progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
    }

    private void OnTriggerExit(Collider other)
    {

        //progress.lastCheckpointPos = transform.position;
        BossGameEVent.current.StartCombatTriggerExit();
        Destroy(this.gameObject);

    }
}

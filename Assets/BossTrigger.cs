using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public int id;
    public ProgressManager progress;
    public BoxCollider box;

    private void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            BossGameEVent.current.StartCombatTriggerExit(id);
            if (box !=null) box.enabled = false;
            Destroy(this.gameObject);
        }





    }
}

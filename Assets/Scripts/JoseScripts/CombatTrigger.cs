using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Paso el trigger");
        BossGameEVent.current.StartCombatTriggerExit();
        Destroy(this.gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivacionCangrejo : MonoBehaviour
{
    public GameObject Cangrejo;
    CangrejoArena scriptCangrejo;
    void Start()
    {
        scriptCangrejo = Cangrejo.GetComponent<CangrejoArena>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scriptCangrejo.enUso = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            scriptCangrejo.enUso = false;
        }
    }
}

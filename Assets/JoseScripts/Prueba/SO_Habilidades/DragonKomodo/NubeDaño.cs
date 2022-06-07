using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDaño : MonoBehaviour
{
    public float dañoPorSegundo;
    public float dañoAlEntrar;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.damagePlayer?.Invoke(dañoPorSegundo);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.damagePlayer?.Invoke(dañoAlEntrar);

        }
    }


}

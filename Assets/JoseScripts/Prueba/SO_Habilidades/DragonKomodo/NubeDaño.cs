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
            PlayerSingleton.Instance.playerCurrentHP -= dañoPorSegundo;
           
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= dañoAlEntrar;

        }
    }


}

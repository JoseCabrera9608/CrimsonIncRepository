using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDaño : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= 1.4f;
           
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= 20f;

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerGa : MonoBehaviour
{
    public string nombreCancion;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play(nombreCancion);
            Destroy(this.gameObject);
        }
        
        
    }
}

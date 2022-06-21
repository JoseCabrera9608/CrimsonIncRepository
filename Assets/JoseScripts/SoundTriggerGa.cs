using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerGa : MonoBehaviour
{
    public string nombreCancion;
    public string cancionAnterior;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Stop(cancionAnterior);
            FindObjectOfType<AudioManager>().Play(nombreCancion);
            Destroy(this.gameObject);
            
        }
        
        
    }
}

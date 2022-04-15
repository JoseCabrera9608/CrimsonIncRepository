using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionArma : MonoBehaviour
{
    public GameObject cangrejo;
    CangrejoArena cangrejoVida;
public int dañoDeArma;
    public int dañoDeArmaPasiva;
    void Start()
    {
        cangrejoVida = cangrejo.GetComponent<CangrejoArena>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Caparazon"))
        {
            Debug.Log("Colisionando con caparazon");

            cangrejoVida.vidaActual -= dañoDeArmaPasiva;
        }

        if (other.gameObject.CompareTag("CuerpoBoss"))
        {
            cangrejoVida.vidaActual -= dañoDeArma;
        }
    }
}

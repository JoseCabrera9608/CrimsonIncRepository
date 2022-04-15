using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionArma : MonoBehaviour
{
    public GameObject cangrejo;
    CangrejoArena cangrejoVida;
public int da�oDeArma;
    public int da�oDeArmaPasiva;
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

            cangrejoVida.vidaActual -= da�oDeArmaPasiva;
        }

        if (other.gameObject.CompareTag("CuerpoBoss"))
        {
            cangrejoVida.vidaActual -= da�oDeArma;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoArmaCangrejo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cangrejo;
    BossCangrejo cangrejoVida;
    public int dañoDeArma;
    public int dañoDeArmaPasiva;
    void Start()
    {
        cangrejoVida = cangrejo.GetComponent<BossCangrejo>();
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

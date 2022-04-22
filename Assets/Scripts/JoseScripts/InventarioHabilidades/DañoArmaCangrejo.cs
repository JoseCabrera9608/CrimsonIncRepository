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
    SkinnedMeshRenderer mesh;
    GameObject cangrejoMesh;
    
    

    void Start()
    {
        cangrejoMesh = GameObject.Find("Patas");
        cangrejoVida = cangrejo.GetComponent<BossCangrejo>();
        mesh = cangrejoMesh.GetComponent<SkinnedMeshRenderer>();
        
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
            StartCoroutine(CambioColor());
            
        }
    }

    IEnumerator CambioColor()
    {
        mesh.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mesh.material.color = Color.grey;

    }
}

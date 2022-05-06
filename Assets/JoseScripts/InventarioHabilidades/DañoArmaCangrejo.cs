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
    int index;
    public Material material;

   // CangrejoArena cangrejoArenaVida;
  //  private GameObject cangrejoArenaObjeto;
  //  SkinnedMeshRenderer meshArena;



    void Start()
    {
        
        cangrejoMesh = GameObject.Find("Patas");
       // cangrejoArenaObjeto = GameObject.Find("BossCangrejoArena");
       // cangrejoArenaVida = cangrejoArenaObjeto.GetComponent<CangrejoArena>();
        cangrejoVida = cangrejo.GetComponent<BossCangrejo>();
        mesh = cangrejoMesh.GetComponent<SkinnedMeshRenderer>();
        index = mesh.materials.Length;
       
       
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
       /* if (other.gameObject.CompareTag("CuerpoBossArena"))
        {
            cangrejoArenaVida.vidaActual -= dañoDeArma;
            
        }*/
    }

    IEnumerator CambioColor()
    {
        mesh.materials[0].color = Color.red;
        mesh.materials[1].color = Color.red;
        mesh.materials[3].color = Color.red;
        yield return new WaitForSeconds(0.5f);
        mesh.materials[0].color = Color.grey;
        mesh.materials[1].color = Color.grey;
        mesh.materials[3].color = Color.grey;
    }

}

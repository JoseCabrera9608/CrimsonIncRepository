using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoArmaCangrejo : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cangrejo;
    BossCangrejo cangrejoVida;
    public float dañoDeArma;
    public float dañoDeArmaPasiva;
    SkinnedMeshRenderer mesh;
    GameObject cangrejoMesh;
    int index;
    public bool hitted;
   
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

            cangrejoVida.vidaActual -= PlayerSingleton.Instance.playerDamage;
        }

        if (other.gameObject.CompareTag("CuerpoBoss"))
        {
            cangrejoVida.sonidoRecibirGolpe.Play();
            cangrejoVida.vidaActual -= PlayerSingleton.Instance.playerDamage;
            StartCoroutine(CambioColor());
            hitted = true;
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

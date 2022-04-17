using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
{
    public float fuerza;
    private GameObject imanObjeto;

    List<Rigidbody> rigPlayer = new List<Rigidbody>();
    public SphereCollider colliderMag;
    float timer;
    bool activado = true;
    float timerAnclado = 3;
    bool capturadoOff;
    public GameObject player;
    Collider colliderPlayer;
    Transform magnetPoint;
    int index;
     Animator anim;
    public GameObject imanesObjetos;

    void Start()
    {
        //timer = 5;
      //imanObjeto = GameObject.Find("Iman");
        magnetPoint = GetComponent<Transform>();
        colliderMag = GetComponent<SphereCollider>();
        anim = imanesObjetos.GetComponent<Animator>();


    }


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5 && activado == true)
        {

            StartCoroutine(Prender());
            activado = false;
            
        }

        
    }

    private void FixedUpdate()
    {
      foreach(Rigidbody rigPlay in rigPlayer)
        {
            rigPlay.AddForce((magnetPoint.position - rigPlay.position) * fuerza * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            rigPlayer.Add(other.GetComponent<Rigidbody>());
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            rigPlayer.Remove(other.GetComponent<Rigidbody>());
        }
    }

    IEnumerator Prender()
    {

        //SkinnedMeshRenderer imanColor = imanesObjetos[index].GetComponent<SkinnedMeshRenderer>();
        //SkinnedMeshRenderer imanColor = imanObjeto.gameObject.GetComponent<SkinnedMeshRenderer>();
        //manColor.material.color = Color.yellow;
        anim.SetTrigger("Giro");
        yield return new WaitForSeconds(4.1f);
        colliderMag.enabled = true;
        yield return new WaitForSeconds(2);
        //anColor.material.color = Color.white;
        colliderMag.enabled = false;
        rigPlayer.Remove(player.GetComponent<Rigidbody>());
        timer = 0;
        activado = true;
    }
}

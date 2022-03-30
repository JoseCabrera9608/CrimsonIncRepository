using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Arena : MonoBehaviour
{
    public GameObject golpeCollider;
    Collider brazoCollider;
    public Animator anim;
    public GameObject nube;
    NubeDaño nubeBool;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        brazoCollider = golpeCollider.GetComponent<Collider>();
        nubeBool = nube.GetComponent<NubeDaño>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Poder1Komodo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Poder2Komodo();
            

        }

    }
    public void Poder1Komodo()
    {
        StartCoroutine(ActivePunchCollider());
    }

    public void Poder2Komodo()
    {
        nubeBool.activado = true;
        anim.SetBool("NubeBool",true);
        StartCoroutine(NubeCorrosiva());
        
    }

    public void Poder3Komodo()
    {

    }

    IEnumerator ActivePunchCollider()
    {
        anim.SetTrigger("Golpear");
        brazoCollider.enabled = true;
        yield return new WaitForSeconds(6.8f);
        brazoCollider.enabled = false;

    }

    IEnumerator NubeCorrosiva()
    {
        
        nube.SetActive(true);
        yield return new WaitForSeconds(6);
        nube.SetActive(false);
        anim.SetBool("NubeBool", false);


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3Arena : MonoBehaviour
{

    public Animator animCalamar;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;
    public GameObject embestidaCollider;
    public GameObject tinta;
    float x;
    float y;
    float z;

    GameObject player;
   

    void Start()
    {
      
       animCalamar = GetComponent<Animator>();
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TentacleStrike();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TirarTinta();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Embestir();
        }

    }
    public void TentacleStrike()
    {
        StartCoroutine(TentacleStrike2());
    }

    public void TirarTinta()
    {
        StartCoroutine(Tinta());
    }

    public void Embestir()
    {
        StartCoroutine(Embestida());
    }

    IEnumerator TentacleStrike2()
    {
        animCalamar.SetTrigger("Golpe");
        brazoDerechoCollider.SetActive(true);
        brazoIzquierdoCollider.SetActive(true);

        yield return new WaitForSeconds(4);
        
        brazoDerechoCollider.SetActive(false);
        brazoIzquierdoCollider.SetActive(false);
        
    }

    IEnumerator Tinta()
    {
        
        x += 22.9f * Time.deltaTime;
        y += 22.9f * Time.deltaTime;
        z += 22.9f * Time.deltaTime;
        tinta.SetActive(true);
        tinta.transform.localScale += new Vector3(x, y, z);
        yield return new WaitForSeconds(3);
        tinta.transform.localScale -= new Vector3(1200f, 1200f, 1200f) *2*  Time.deltaTime;
        yield return new WaitForSeconds(1.4f);
        tinta.SetActive(false);
        
    }
    IEnumerator Embestida()
    {
        animCalamar.SetTrigger("Embestir");
        embestidaCollider.SetActive(true);
        yield return new WaitForSeconds(4);
        embestidaCollider.SetActive(false);
        yield return new WaitForSeconds(3);
    }
   
}

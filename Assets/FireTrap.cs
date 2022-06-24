using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{

    public float timer;
    public float cd;
    public GameObject firepoint;
    public GameObject fire;
    public GameObject colliderobj;
    public float firelifetime;
    public float intervalo;
    public CapsuleCollider col;

    public float coltimer;
    public float colsize;
    public float maxcolsize;

    public string traptype;

    // Start is called before the first frame update

    private void Awake()
    {
        maxcolsize = colliderobj.transform.localScale.z;
    }

    void Start()
    {
        colliderobj.transform.localScale = new Vector3(colliderobj.transform.localScale.x, colliderobj.transform.localScale.y, 0.1f);
        timer = firelifetime + intervalo;
        cd = firelifetime + intervalo;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        Spawn();
    }
    public void Spawn()
    {

        if (firepoint != null && timer >= cd)
        {
            
            

            fire.SetActive(true);

            timer = 0;
        }

        if (firelifetime < timer)
        {   
            colsize = 0;
            fire.SetActive(false);
        }

        if (timer > 0 && timer < firelifetime && colsize< maxcolsize)
        {
            colsize += Time.deltaTime;
            colliderobj.transform.localScale = new Vector3(colliderobj.transform.localScale.x, colliderobj.transform.localScale.y, colsize);
        }


    }
}

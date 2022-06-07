using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercoElectrico : MonoBehaviour
{
    public float damage;
    [SerializeField] bool desactivar;
    Animator anim;
    public int id;
    BoxCollider colliderElectrico;
    void Start()
    {
        anim = GetComponent<Animator>();
        colliderElectrico = GetComponent<BoxCollider>();
        //  BossGameEVent.current.combatTriggerExit += StartChase;
        BossGameEVent.current.Conexion += Desactivar;
    }

    // Update is called once per frame
    void Update()
    {

    }
        

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Detectado Gaa");
            PlayerSingleton.Instance.playerCurrentHP -= 10000;
        }
    }

    public void Desactivar(int id)
    {
        if (id == this.id)
        {
            anim.SetTrigger("Apagar");
            colliderElectrico.enabled = false;
        }
        
         /*   if (desactivar == true)
        {
            anim.SetTrigger("Apagar");
            colliderElectrico.enabled = false;

        }*/
    }
}
    

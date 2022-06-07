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
        

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= 100;
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
    

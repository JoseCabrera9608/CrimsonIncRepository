using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDConexion : MonoBehaviour
{
    public int id;
    public bool destruido;
    public int vida;
    MeshRenderer meshEstaca;
    void Start()
    {
        meshEstaca = this.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vida <= 0)
        {
            destruido = true;
            Destroy(this.gameObject, 1f);
        }

        if(destruido == true)
        {
            BossGameEVent.current.Conectar(id);
        }

        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerWeapon"))
        {
            StartCoroutine(CambioColor());
            vida -= 10;
        }
    }
    IEnumerator CambioColor()
    {
        meshEstaca.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        meshEstaca.material.color = Color.blue;

    }
}

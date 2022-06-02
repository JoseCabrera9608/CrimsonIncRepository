using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloCaliente : MonoBehaviour
{
    float time;
    public BoxCollider colliderSuelo;
    public float damage;
    public bool desactivado;
    public float tiempoDeActivacion;
    public bool quemar;
    public GameObject humo;
    GameObject player;
    Movement movimientoJugador;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        colliderSuelo = GetComponent<BoxCollider>();
        movimientoJugador = player.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= tiempoDeActivacion)
        {
            quemar = true;

        }

        if (quemar == true)
        {
            StartCoroutine(ActivarCollider());
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            movimientoJugador.runSpeed = 5;
            movimientoJugador.walkSpeed = 5;
        }
    }


    IEnumerator ActivarCollider()
    {
        humo.SetActive(true);
        colliderSuelo.enabled = true;
        yield return new WaitForSeconds(5f);
        humo.SetActive(false);
        colliderSuelo.enabled = false;
        time = 0;
        quemar = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
            PlayerSingleton.Instance.playerHitted = true;
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        movimientoJugador.runSpeed = 9;
        movimientoJugador.walkSpeed = 9;
    }

    

}


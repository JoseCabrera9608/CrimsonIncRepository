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
    public GameObject player;
    Movement movimientoJugador;
    void Start()
    {
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
            movimientoJugador.movSpeed -= 2;
        }
    }


    IEnumerator ActivarCollider()
    {
        humo.SetActive(true);
        colliderSuelo.enabled = true;
        yield return new WaitForSeconds(6f);
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
        movimientoJugador.movSpeed = 9;
    }

    

}


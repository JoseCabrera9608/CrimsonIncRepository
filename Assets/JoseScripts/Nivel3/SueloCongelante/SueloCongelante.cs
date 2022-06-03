using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloCongelante : MonoBehaviour
{
    float time;
    public BoxCollider colliderSuelo;
    public bool desactivado;
    public float tiempoDeActivacion;
    public bool congelar;
    public bool saved;
    GameObject player;
    public GameObject humo;
    Movement movimientoJugador;
    public float tempwalkspeed;
    public float temprunspeed;
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
            congelar = true;

        }

        if (congelar == true)
        {
            StartCoroutine(ActivarCollider());
        }

        if(congelar == false)
        {
            //movimientoJugador.runSpeed = 9;
            //movimientoJugador.walkSpeed = 9;
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
        congelar = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") &&congelar == true)
        {
            if (saved == false)
            {
                temprunspeed = movimientoJugador.runSpeed;
                tempwalkspeed = movimientoJugador.walkSpeed;
                saved = true;
            }
            else
            {
                movimientoJugador.runSpeed = 3.8f;
                movimientoJugador.walkSpeed = 3.8f;
            }
        

        }

    }
    private void OnTriggerExit(Collider other)
    {
        movimientoJugador.runSpeed = temprunspeed;
        movimientoJugador.walkSpeed = tempwalkspeed;
        saved = false;
    }

    


}

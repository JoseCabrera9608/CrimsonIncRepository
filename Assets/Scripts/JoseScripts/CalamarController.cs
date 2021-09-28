using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CalamarController : MonoBehaviour
{
    GameObject player;
    private NavMeshAgent agente;
    private bool onChase = false;
    AudioSource growl;
    private bool growling = false;
    void Start()
    {
        BossGameEVent.current.combatTriggerExit += StartChase; //El metodo se suscribe al BossGameEvent
        agente = GetComponent<NavMeshAgent>();
        growl = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        growl.Stop();
    }
    private void Update()
    {
        if (onChase == true)
        {
            agente.SetDestination(player.transform.position);
            
        }
      
    }
    private void StartChase()
    {
        onChase = true;
        growl.Play();
    }

    private void OnDestroy() //Metodo por si se destruye el objeto se desuscriba del BossGameEvent para evitar errores
    {
        BossGameEVent.current.combatTriggerExit -= StartChase;
    }
}

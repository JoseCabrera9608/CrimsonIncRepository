using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CalamarController : MonoBehaviour
{
    [SerializeField] private bool onChase = false;
    [SerializeField] private float timerCooldown;
    [SerializeField] private bool canCast;
    [SerializeField] private float distance;
    //public CalamarVisuals calamarAnimations;
    GameObject player;
    private NavMeshAgent agente;
    public bool electrocutado;



    void Start()
    {
        BossGameEVent.current.combatTriggerExit += StartChase; //El metodo se suscribe al BossGameEvent
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {


        if (onChase == true) // Cuando se activa el boss
        {
            agente.SetDestination(player.transform.position);

            distance = Vector3.Distance(transform.position, player.transform.position);
            timerCooldown += Time.deltaTime;
            if (timerCooldown >= 5f)
            {
                canCast = true;
                timerCooldown = 0;

            }
            if (distance >= 5 && canCast == true)
            {

                StartCoroutine(TentacleStrike());
                canCast = false;
            }

            if (distance >= 2 && canCast == true)
            {
                electrocutado = true;
            }

        }
    }
    private void StartChase()
    {
        onChase = true;
    }

    IEnumerator TentacleStrike()
    {
        agente.speed = 0;
        //calamarAnimations.TentacleStrikeAnimation();
        yield return new WaitForSeconds(3);
        agente.speed = 3.5f;
    }


    private void OnDestroy() //Metodo por si se destruye el objeto se desuscriba del BossGameEvent para evitar errores
    {
        BossGameEVent.current.combatTriggerExit -= StartChase;
    }
}

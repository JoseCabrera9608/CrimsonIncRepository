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
    public Animator animCalamar;
    public GameObject[] spawns;
    GameObject chosenSpawn;
    public GameObject misile;
    int index;
    
    GameObject player;
    public NavMeshAgent agente;
    public bool electrocutado;
    public bool tentacleStrikeBool = false;
    public bool tintaDisparada = false;
    public bool embistiendo = false;
    public bool lanzarMisiles = false;



    void Start()
    {
        animCalamar = GetComponent<Animator>();
        BossGameEVent.current.combatTriggerExit += StartChase; //El metodo se suscribe al BossGameEvent
        agente = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {


        if (onChase == true) // Cuando se activa el boss
        {
            agente.SetDestination(player.transform.position);

            /*distance = Vector3.Distance(transform.position, player.transform.position);
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
            }*/

        }

        if (tentacleStrikeBool == true)
        {
            StartCoroutine(TentacleStrike2());
        }

        if (tintaDisparada == true)
        {
            
            StartCoroutine(TintaCambioColor());
        }
        if(embistiendo == true)
        {
            StartCoroutine(Embestida());
        }
        if(lanzarMisiles == true)
        {
            StartCoroutine(Misiles());
            lanzarMisiles = false;
        }

    }
    private void StartChase()
    {
        onChase = true;
    }

    IEnumerator TentacleStrike2()
    {
        agente.speed = 0;
        //calamarAnimations.TentacleStrikeAnimation();
        yield return new WaitForSeconds(10);
        agente.speed = 6f;
    }
    IEnumerator TintaCambioColor()
    {
        agente.speed = 0;
        yield return new WaitForSeconds(3);
        GameObject calamar = GameObject.Find("Blocking Calamar");
        MeshRenderer calamarColor = calamar.GetComponent<MeshRenderer>();
        calamarColor.material.color = Color.blue;
        tintaDisparada = false;
        agente.speed = 6f;
        
    }
    IEnumerator Embestida()
    {
        yield return new WaitForSeconds(5);
        animCalamar.SetBool("Trompo", false);
        agente.speed = 6f;
    }

    IEnumerator Misiles()
    {
        agente.speed = 0f;
        //animacion idk
        index = 0;
        chosenSpawn = spawns[index];
        GameObject temporalMisile = Instantiate(misile);
        temporalMisile.transform.position = chosenSpawn.transform.position;
        Debug.Log("Creo misiles");
        yield return new WaitForSeconds(3);
    }


    private void OnDestroy() //Metodo por si se destruye el objeto se desuscriba del BossGameEvent para evitar errores
    {
        BossGameEVent.current.combatTriggerExit -= StartChase;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CalamarController : MonoBehaviour
{
    public GameObject barraHUDEnemigo;
    private GameObject calamar;
    [SerializeField] private bool onChase = false;
    [SerializeField] private float timerCooldown;
    [SerializeField] private bool canCast;
    [SerializeField] private float distance;
    public Animator animCalamar;
    public GameObject[] spawns;
    GameObject chosenSpawn;
    public GameObject misile;
    int index;
    public int health;
    public bool hitted;

    public GameObject embestidaCollider;
    public GameObject brazoDerechoCollider;
    public GameObject brazoIzquierdoCollider;
    Collider brazoDer;
    Collider brazoIzq;
    Collider embestida;
    
    GameObject player;
    public NavMeshAgent agente;
    public bool electrocutado;
    public bool tentacleStrikeBool = false;
    public bool tintaDisparada = false;
    public bool embistiendo = false;
    public bool lanzarMisiles = false;

    public GameObject tinta;

    public GameObject audioManager;

    void Start()
    {
        calamar = GameObject.Find("Calamarsito");
        /*brazoDer = brazoDerechoCollider.GetComponent<Collider>();
        brazoIzq = brazoIzquierdoCollider.GetComponent<Collider>();
        embestida = embestidaCollider.GetComponent<Collider>();*/
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

        }

        if (tentacleStrikeBool == true)
        {
            StartCoroutine(TentacleStrike2());
            tentacleStrikeBool = false;
        }

        if (tintaDisparada == true)
        {
            SkinnedMeshRenderer calamarColor = calamar.GetComponent<SkinnedMeshRenderer>();
            calamarColor.material.color = Color.red;
            StartCoroutine(TintaCambioColor());
        }
        if(embistiendo == true)
        {
            StartCoroutine(Embestida());
            embistiendo = false;
            

        }
        if(lanzarMisiles == true)
        {
            StartCoroutine(Misiles());
            lanzarMisiles = false;
        }

        if(health <= 20)
        {
            Destroy(embestidaCollider);
            Destroy(audioManager);
            agente.speed = 0;
            animCalamar.SetTrigger("Ruego");
            if(health <= 0)
            {
                StartCoroutine(Muerte());
            }
        }

    }
    private void StartChase()
    {
        onChase = true;
        animCalamar.SetTrigger("Inicio");
        barraHUDEnemigo.SetActive(true);
    }

    IEnumerator TentacleStrike2()
    {
        agente.speed = 0;
        yield return new WaitForSeconds(1.1f);
        FindObjectOfType<AudioManager>().Play("Pinchazos");
        brazoDerechoCollider.SetActive(true);
        brazoIzquierdoCollider.SetActive(true);
     
        yield return new WaitForSeconds(7);
      
        brazoDerechoCollider.SetActive(false);
        brazoIzquierdoCollider.SetActive(false);
        agente.speed = 6f;
    }
    IEnumerator TintaCambioColor()
    {
        
        agente.speed = 0;
        
        tinta.SetActive(true);
        tinta.transform.localScale += new Vector3(5, 5, 5)*22f * Time.deltaTime;
        yield return new WaitForSeconds(3);
        tinta.transform.localScale -= new Vector3(12, 12, 12) * 21f * Time.deltaTime;
        tinta.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1.5f);
        tinta.SetActive(false);
        tintaDisparada = false;
        agente.speed = 6f;
        
    }
    IEnumerator Embestida()
    {
        agente.speed = 0f;
        FindObjectOfType<AudioManager>().Play("Embestida");
        embestidaCollider.SetActive(true);
        yield return new WaitForSeconds(4);
        embestidaCollider.SetActive(false);
        agente.speed = 5f;
       
    }

    IEnumerator Misiles()
    {
        agente.speed = 0f;
        animCalamar.SetTrigger("Misile");
        index = 1;
        chosenSpawn = spawns[index];
        FindObjectOfType<AudioManager>().Play("Misiles");
        GameObject temporalMisile = Instantiate(misile);
        temporalMisile.transform.position = chosenSpawn.transform.position;
        Debug.Log("Creo misiles");
        yield return new WaitForSeconds(3);
        agente.speed = 5f;
    }

    IEnumerator Muerte()
    {
        animCalamar.SetTrigger("Muerte");
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);

    }


    private void OnDestroy() //Metodo por si se destruye el objeto se desuscriba del BossGameEvent para evitar errores
    {
        BossGameEVent.current.combatTriggerExit -= StartChase;
    }
}

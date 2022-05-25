using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorgonopsiaBoss : MonoBehaviour
{
    [SerializeField] public Gstates currentAction;
    private GorgonopsiaStats stats;
    private GameObject player;
    [HideInInspector]public float currentDamageValue;

    [Header("===============OBJETOS A CREAR=================")]
    [SerializeField] private GameObject alientoCalor;
    [SerializeField] private GameObject rugidoExplosivo;
    [SerializeField] private GameObject bombaJaeger;
    [SerializeField] private GameObject bombas360;

    [Header("===============CONTROL VARIABLES===============")]
    public bool isActing;
    [SerializeField] private Transform bombaJaegerPosIzquierda;
    [SerializeField] private Transform bombaJaegerPosDerecha;
    [SerializeField] private float timer1;
    void Start()
    {
        stats = GetComponent<GorgonopsiaStats>();
        player = FindObjectOfType<PlayerStatus>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
        RotateToPlayer();
        currentDamageValue = stats.gorgoDamages[currentAction];
    }
    private void StateMachine()
    {
        switch (currentAction)
        {
            case Gstates.idle:
                break;

            case Gstates.cargaCalor:
                break;

            case Gstates.embestidaFrenetica:
                break;

            case Gstates.blink:
                HandleBlink(stats.defaultPositionVector, 0.5f);
                break;

            case Gstates.rugidoExplosivo:
                HandleRugidoExplosivo();
                break;

            case Gstates.bomba360:
                HandleBombas360();
                break;

            case Gstates.alientoCalor:
                HandleAlientoDeCalor();
                break;

            case Gstates.bombaJaeger:
                HandleBombaJaeger();
                break;          
        }
    }
    //========================HANDLE METHODS=========================
    public void HandleRugidoExplosivo()
    {
        if (isActing == false)
        {
            GameObject obj = Instantiate(rugidoExplosivo);
            obj.transform.position = transform.position+new Vector3(0,0.1f,0);
            RugidoExplosivo script = obj.GetComponent<RugidoExplosivo>();
            script.damage = stats.rugidoExplosivoDamage;
            script.dimension = stats.rugidoExplosivoRadius;
            script.chargeTime = stats.rugidoExplosivoChargeTime;
            StartCoroutine(ResetActing(stats.rugidoExplosivoChargeTime));
            //currentAction = Gstates.idle;
        }
        isActing = true;
    }
    public void HandleBombas360()
    {
        timer1 += Time.deltaTime;
        if (isActing == false) StartCoroutine(ResetActing(stats.bomba360Amount * stats.bomba360TimeToDamage));
        isActing = true;
        for(int i = 0; i < stats.bomba360Amount; i++)
        {
            if (timer1 >= stats.bomba360TimeToDamage)
            {
                timer1 = 0;
                GameObject obj = Instantiate(bombas360);
                obj.transform.position = transform.position + new Vector3(0, 0.1f, 0);
                Bomba360 script = obj.GetComponent<Bomba360>();
                script.damage = stats.bombas360Damage;
                script.timeToDamage = stats.bomba360TimeToDamage;
                script.distanceTreshold = stats.bomba360DistanceTreshold;
            }
        }
        
    }
    public void HandleAlientoDeCalor()
    {
        if (isActing == false) alientoCalor.SetActive(true);

        AlientoCalor script = alientoCalor.GetComponent<AlientoCalor>();
        script.alientoCalorChargeTime = stats.alientoCalorChargeTime;
        script.alientoCalorRotationDuration = stats.alientoCalorRotationDuration;
        script.alientoCalorRange = stats.alientoCalorRange;
        script.alientoCalorAngle = stats.alientoCalorAngle;
        script.alientoCalorDamage = stats.alientoCalorDamage;

        if (isActing == false) StartCoroutine(ResetActing(stats.alientoCalorChargeTime +
               stats.alientoCalorRotationDuration+1));
        isActing = true;

        //currentAction = Gstates.idle;
    }
    public void HandleBombaJaeger()
    {
        for(int i = 0; i < 2; i++)
        {
            GameObject obj = Instantiate(bombaJaeger);
            if (i == 0)
            {
                obj.transform.position = bombaJaegerPosIzquierda.position;
                obj.transform.localEulerAngles= new Vector3(-45, 90, 0);
            }
            else
            {
                obj.transform.position = bombaJaegerPosDerecha.position;
                obj.transform.localEulerAngles = new Vector3(-45, -90, 0);
            }
            obj.GetComponent<Rigidbody>().AddForce(obj.transform.forward * 2, ForceMode.VelocityChange);
            BombaJaeger script = obj.GetComponent<BombaJaeger>();
            script.damage = stats.bombasJaegerDamage;
            script.speed = stats.bombaJaegerSpeed;
            script.distanceTreshHold = stats.bombaJaegerDistanceTreshHold;
            script.explotionRadius = stats.bombaJaegerExplotionRadius;
            script.timeToAct = stats.bombaJaegerTimeToAct;
        }

        currentAction = Gstates.idle;
    }
    public void HandleBlink(Vector3 destination,float blinkTime)
    {
        //stats.defaultPositionVector = stats.defaultPosition.position;
        if (isActing == false)
        {
            //Desactivar renderer de objetos 
            foreach(MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
            {
                renderer.enabled = false;
            }
            //Mover al destino y rotar hacia el jugador
            transform.position = destination;
            transform.LookAt(player.transform.position);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);

            //Activar renderer de objetos 
            StartCoroutine(ResetRenderer(blinkTime));

            currentAction = Gstates.idle;
        }
        isActing = true;
        StartCoroutine(ResetActing(0));
    }

    //========================UTILITY METHODS========================
    private void RotateToPlayer()
    {
        Quaternion direction = Quaternion.LookRotation(player.transform.position - transform.position);
        direction.x = 0;

        if (isActing==false)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction,
            stats.rotationSpeed * Time.deltaTime);
        }

    }
    public IEnumerator ResetActing(float time)
    {
        yield return new WaitForSeconds(time);
        currentAction = Gstates.idle;
        timer1 = 0;
        isActing = false;
    }
    public IEnumerator ResetRenderer(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GorgonopsiaBoss : MonoBehaviour
{
    #region EstadoActual

    [Header("===============ESTADO ACTUAL=================")]
    [SerializeField] public Gstates currentAction;
    [HideInInspector] public float currentDamageValue;
    public int currentHeatCharges;
    public bool leftLegOn;
    public bool rightLegOn;
    private GorgonopsiaStats stats;
    private GameObject player;

    #endregion
    public GameObject EnemyBar;
    #region Objetos a crear
    [Header("===============OBJETOS A CREAR=================")]
    [SerializeField] private GameObject alientoCalor;
    [SerializeField] private GameObject rugidoExplosivo;
    [SerializeField] private GameObject bombaJaeger;
    [SerializeField] private GameObject bombas360;
    [SerializeField] private GameObject embestidaFreneticaParent;
    [SerializeField] private GameObject fireObject;
    [SerializeField] private GameObject[] legs;
    [SerializeField] private GameObject[] cargaCalor;
    [SerializeField] private GameObject legParent;
    [SerializeField] private BoxCollider col;

    #endregion

    #region variables de control
    [Header("===============CONTROL VARIABLES===============")]
    public bool isActing;
    public bool evaluatingAttack;
    [SerializeField] private Transform bombaJaegerPosIzquierda;
    [SerializeField] private Transform bombaJaegerPosDerecha;
    [SerializeField] private float timer1;
    [SerializeField] private bool bool1;
    [SerializeField] private bool bool2;
    private Vector3 originalPos;
    public Tween embestidaTween;
    #endregion

    #region probabilidades
    [Header("===============ATTACK PROBABILITIES===============")]
    [SerializeField] public GorgonopsiaAttackProbabilities[] attackProbabilities;
    [SerializeField] public GorgonopsiaAttackProbabilities[] bombaJaegerProbability;
    #endregion
    void Start()
    {
        stats = GetComponent<GorgonopsiaStats>();
        player = FindObjectOfType<PlayerStatus>().gameObject;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.isAlive == true&&stats.isActive)
        {
            StateMachine();
            RotateToPlayer();
            UpdateCargaCalorVisuals();
        }
        
        //currentDamageValue = stats.gorgoDamages[currentAction];
    }
    private void StateMachine()
    {
        switch (currentAction)
        {
            case Gstates.idle:
                if (evaluatingAttack == false) HandleIdle(false,Gstates.idle);
                break;

            case Gstates.cargaCalor:
                HandleCargasDeCalor();
                break;

            case Gstates.embestidaFrenetica:
                HandleEmbestidaFrenetica();
                break;

            case Gstates.blink:
                //HandleBlink(stats.defaultPositionVector, 0.5f);
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
    public void HandleIdle(bool specificAttack,Gstates desiredAttack)
    {
        if (specificAttack && evaluatingAttack == false)
        {
            StartCoroutine(SetSpecificAttack(desiredAttack));
        }
        else if (specificAttack == false && evaluatingAttack == false)
        {
            if (transform.position != stats.defaultPositionVector)
            {
                HandleBlink(stats.defaultPositionVector, stats.blinkDefaultTime);
            }               
            StartCoroutine(AttackEvaluation());
        }
    }
    public IEnumerator SetSpecificAttack(Gstates attack)
    {
        evaluatingAttack = true;
        yield return new WaitForSeconds(stats.evaluationTime);
        currentAction = attack;
        evaluatingAttack = false;
    }
    public IEnumerator AttackEvaluation()
    {
        if (stats.on50Health && SpawnJaegerBombs()) HandleBombaJaeger(); 
        evaluatingAttack = true;
        yield return new WaitForSeconds(stats.evaluationTime);
        int random = Random.Range(0, 101);
        Debug.Log("Attack number is= " + random);
        for(int i = 0; i < attackProbabilities.Length; i++)
        {
            if (random >= attackProbabilities[i].minProbability && random <= attackProbabilities[i].maxProbability)
            {
                currentAction = attackProbabilities[i].nextAttack;
                break;
            }
        }

        evaluatingAttack = false;
    }
    public void HandleRugidoExplosivo()
    {
        if (isActing == false)
        {
            GameObject obj = Instantiate(rugidoExplosivo);
            obj.transform.position = transform.position+new Vector3(0,0.1f,0);
            RugidoExplosivo script = obj.GetComponent<RugidoExplosivo>();
            script.damage = stats.rugidoExplosivoDamage;
            script.dimension = stats.rugidoExplosivoRadius;
            if (stats.attackSpeedBonus) script.chargeTime = stats.rugidoExplosivoChargeTime - (stats.rugidoExplosivoChargeTime * stats.generalAttackSpeedBonus);
            else script.chargeTime = stats.rugidoExplosivoChargeTime;


            if (currentHeatCharges!=2)StartCoroutine(TryToChargeHeat(stats.rugidoExplosivoChargeTime));
            else StartCoroutine(ResetActing(stats.rugidoExplosivoChargeTime));
            //currentAction = Gstates.idle;
        }
        isActing = true;
    }
    public void HandleBombas360()
    {
        timer1 += Time.deltaTime;
        if (isActing == false)
        {
            
            if (currentHeatCharges != 2) StartCoroutine(TryToChargeHeat(stats.bomba360Amount * stats.bomba360TimeToDamage));
            else StartCoroutine(ResetActing(stats.bomba360Amount * stats.bomba360TimeToDamage));
        }

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
                if (stats.attackSpeedBonus) script.timeToDamage = stats.bomba360TimeToDamage - (stats.bomba360TimeToDamage * stats.generalAttackSpeedBonus);
                else script.timeToDamage = stats.bomba360TimeToDamage;
                script.distanceTreshold = stats.bomba360DistanceTreshold;
            }
        }
        
    }
    public void HandleAlientoDeCalor()
    {
        if (isActing == false)
        {
            alientoCalor.SetActive(true); 
           
            if (currentHeatCharges != 2) StartCoroutine(TryToChargeHeat(stats.alientoCalorChargeTime + stats.alientoCalorRotationDuration + 1));
            else StartCoroutine(ResetActing(stats.alientoCalorChargeTime + stats.alientoCalorRotationDuration + 1));

        }
        isActing = true;
        AlientoCalor script = alientoCalor.GetComponent<AlientoCalor>();
        script.alientoCalorChargeTime = stats.alientoCalorChargeTime;
        if (stats.attackSpeedBonus) script.alientoCalorRotationDuration = stats.alientoCalorRotationDuration - (stats.alientoCalorRotationDuration * stats.generalAttackSpeedBonus);
        else script.alientoCalorRotationDuration = stats.alientoCalorRotationDuration;
        script.alientoCalorRange = stats.alientoCalorRange;
        script.alientoCalorAngle = stats.alientoCalorAngle;
        script.alientoCalorDamage = stats.alientoCalorDamage;

        //if (isActing == false)
        //{
        //    StartCoroutine(ResetActing(stats.alientoCalorChargeTime +stats.alientoCalorRotationDuration + 1));
        //    if (currentHeatCharges != 2) StartCoroutine(TryToChargeHeat(stats.alientoCalorChargeTime + stats.alientoCalorRotationDuration + 1));
        //}

        //isActing = true;

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
            obj.GetComponent<Rigidbody>().AddForce(transform.forward * 4, ForceMode.VelocityChange);
            BombaJaeger script = obj.GetComponent<BombaJaeger>();
            script.damage = stats.bombasJaegerDamage;
            if (stats.attackSpeedBonus) script.speed = stats.bombaJaegerSpeed + (stats.bombaJaegerSpeed * stats.generalAttackSpeedBonus);
            else script.speed = stats.bombaJaegerSpeed;
            script.distanceTreshHold = stats.bombaJaegerDistanceTreshHold;
            script.explotionRadius = stats.bombaJaegerExplotionRadius;
            script.timeToAct = stats.bombaJaegerTimeToAct;
        }

        //currentAction = Gstates.idle;
    }
    public void HandleBlink(Vector3 destination,float blinkTime)
    {

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
        //Mover al destino y rotar hacia el jugador
        transform.position = new Vector3(destination.x,originalPos.y,destination.z);
        var lookPos = player.transform.position;
        //lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //Activar renderer de objetos 
        StartCoroutine(ResetRenderer(blinkTime));
    }
    public void HandleEmbestidaFrenetica()
    {
        if(bool1==true)timer1 += Time.deltaTime;

        if (isActing == false)
        {           
            if (currentHeatCharges != 2) StartCoroutine(TryToChargeHeat((stats.embestidaFreneticaAnimationDuration
            + stats.embestidaFreneticaDelay + stats.embestidaFreneticaDuration) * stats.embestidaFreneticaAmount));
            else
                StartCoroutine(ResetActing((stats.embestidaFreneticaAnimationDuration
            + stats.embestidaFreneticaDelay + stats.embestidaFreneticaDuration) * stats.embestidaFreneticaAmount));

        }

        isActing = true;

        //====================SET DAMAGES=======================
        foreach(EmbestidaFreneticaDamageTrigger obj in
            embestidaFreneticaParent.GetComponentsInChildren<EmbestidaFreneticaDamageTrigger>())
        {
            obj.damage = stats.embestidaFreneticaDamage;
        }

        //===================MOVE BOSS FORWARD====================
        for(int i = 0; i < stats.embestidaFreneticaAmount; i++)
        {
            //===============BLINK==============================
            if (bool2 == true)
            {
                bool2 = false;                
                HandleBlink(TryGetEmbestidaPosition(), stats.blinkDefaultTime);
            }
            //========================MOTION=================================
            if (timer1 >= stats.embestidaFreneticaDelay+stats.blinkDefaultTime)
            {
                timer1 = 0;
                bool1 = false;
                embestidaFreneticaParent.SetActive(true);
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position) * 2;
                //MOVE 
                float duration;
                if(stats.attackSpeedBonus) duration= stats.embestidaFreneticaDuration-(stats.embestidaFreneticaDuration*stats.generalAttackSpeedBonus);
                else duration= stats.embestidaFreneticaDuration;

                //Vector3 desiredPosition = new Vector3(player.transform.position.x*2, transform.position.y,
                //    player.transform.position.z*2);
                embestidaTween = transform.DOMove(transform.position+(transform.forward*distanceToPlayer),
                    duration,false).SetEase(Ease.InExpo).OnComplete(ResetEmbestidaParentColliders);
                embestidaTween.Play();
            }           
        }
        
    }
    public Vector3 TryGetEmbestidaPosition()
    {
        Vector3 playerPos = player.transform.position;

        for(int i = 0; i < 500; i++)
        {
            float xRandom;
            float zRandom;

            if (Random.value < 0.5f)
            {
                if (Random.value < 0.5f) xRandom = -1;
                else xRandom = 1;

                if (Random.value < 0.5f) zRandom = -1;
                else zRandom = 1;
            }
            else
            {
                if (Random.value < 0.5f)
                {
                    xRandom = 0;
                    if (Random.value < 0.5f) zRandom = -1;
                    else zRandom = 1;
                }
                else
                {
                    zRandom = 0;
                    if (Random.value < 0.5f) xRandom = -1;
                    else xRandom = 1;
                }
            }

           

            Vector3 desiredPosition = new Vector3(playerPos.x + (stats.embestidaFreneticaBlinkDistance * xRandom)
            , transform.position.y, playerPos.z + (stats.embestidaFreneticaBlinkDistance * zRandom));

            RaycastHit hit;
            if(Physics.Raycast(desiredPosition+Vector3.up,Vector3.down,out hit, 10))
            {
                if (hit.point != null)
                {
                    return desiredPosition;
                    
                }
            }
        }
        return stats.defaultPositionVector;

        

    }
    public void ResetEmbestidaParentColliders()
    {
        embestidaFreneticaParent.SetActive(false);
        bool1 = true;
        bool2 = true;
    }
    public void HandleCargasDeCalor()
    {
        if (isActing == false)
        {
            StartCoroutine(ResetActing(stats.cargaCalorChargeTime));
            StartCoroutine(DeactivateLegColliders(stats.cargaCalorChargeTime));
            legParent.SetActive(true);

            leftLegOn = true;
            rightLegOn = true;
            legs[0].SetActive(true);
            legs[1].SetActive(true);
            currentHeatCharges=2;
            foreach(GameObject obj in legs)
            {
                obj.GetComponent<CargaCalorLegIdentifier>().health = 2;
            }

        }
        isActing = true;      
    }
    public IEnumerator DeactivateLegColliders(float time)
    {
        yield return new WaitForSeconds(time);
        legParent.SetActive(false);
    }
    public void UpdateCargaCalorVisuals()
    {
        if (leftLegOn)
        {
            cargaCalor[0].SetActive(true);
        }
        else
        {
            cargaCalor[0].SetActive(false);
        }


        if (rightLegOn)
        {
            cargaCalor[1].SetActive(true);
        }
        else
        {
            cargaCalor[1].SetActive(false);
        }

        switch (currentHeatCharges)
        {
            case 1:
                stats.fireBonus = true;
                stats.attackSpeedBonus = false;
                break;

            case 2:
                stats.fireBonus = true;
                stats.attackSpeedBonus = true;
                break;

            default:
                stats.fireBonus = false;
                stats.attackSpeedBonus = false;
                break;
        }
    }

    public void HandleDeath()
    {
        if (stats.isAlive)
        {
            transform.DORotate(new Vector3(-180, transform.localEulerAngles.y, transform.localEulerAngles.z)
            , 1, RotateMode.Fast).SetEase(Ease.Flash);
            transform.DOJump(new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), 6, 1, 1, false);
        }
        stats.isAlive = false;      
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
    public IEnumerator TryToChargeHeat(float time)
    {
        yield return new WaitForSeconds(time);
        isActing = false;
        currentAction = Gstates.cargaCalor;
    }
    public bool SpawnJaegerBombs()
    {
        int random = Random.Range(0, 101);
        if (random <= bombaJaegerProbability[0].maxProbability)
        {
            return true;
        } 
        else return false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            if (stats.health <= PlayerSingleton.Instance.playerDamage&&stats.isAlive)
            {
                stats.health -= PlayerSingleton.Instance.playerDamage;
                HandleDeath();
            }
            else stats.health -= PlayerSingleton.Instance.playerDamage;
            if (stats.health <= stats.maxHP / 2) stats.on50Health = true;
        }

        if (other.CompareTag("Player") && stats.isActive == false)
        {
            stats.isActive = true;
            if (EnemyBar != null) EnemyBar.SetActive(true);
            col.enabled = false; 
        }
    }
}


[System.Serializable]
public class GorgonopsiaAttackProbabilities
{
    [SerializeField] public Gstates nextAttack;
    public float minProbability;
    public float maxProbability;
}

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
    [SerializeField] private GameObject embestidaFeedback;

    #endregion

    public AudioSource audioSource;
    public AudioSource music;
    public AudioSource hitSound;
    public bool activateTargetVfx;

    public int embestidaCounter;
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

    private GorgonopsiaAnims anims;

    [SerializeField] private List<SkinnedMeshRenderer> meshes;

    [SerializeField] private bool onArena;

    public bool canRotate;
    private float rotationSpeed;
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
        anims = GetComponent<GorgonopsiaAnims>();
        canRotate = true;
        originalPos = transform.position;
        rotationSpeed = stats.defaultRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.isAlive == true&&stats.isActive)
        {
            StateMachine();
            RotateToPlayer();
            UpdateCargaCalorVisuals();
            LegCheck();
        }

        if (onArena == true) Arena();
        //currentDamageValue = stats.gorgoDamages[currentAction];
    }

    private void Arena()
    {
        if (isActing) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentAction = Gstates.cargaCalor;
            anims.SetAnimationTrigger("cargaCalor");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentAction = Gstates.embestidaFrenetica;
            anims.SetAnimationTrigger("embestidaFrenetica");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HandleBlink(originalPos, 1);           
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentAction = Gstates.rugidoExplosivo;
            anims.SetAnimationTrigger("rugidoExplosivo");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentAction = Gstates.bomba360;
            anims.SetAnimationTrigger("bomba360");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentAction = Gstates.alientoCalor;
            anims.SetAnimationTrigger("alientoCalor");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine(BombaJaegerVFXTimer());
            HandleBombaJaeger();
        }

    }
    private void StateMachine()
    {
        switch (currentAction)
        {
            case Gstates.idle:
                if (evaluatingAttack == false && onArena==false) HandleIdle(false,Gstates.idle);

                DeactivateEmbestidaFeedback();
                break;

            case Gstates.embestidaFrenetica:
                HandleEmbestidaFrenetica();
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
        //canRotate = true;
        yield return new WaitForSeconds(stats.evaluationTime);

        int random = Random.Range(0, 101);
      
        for (int i = 0; i < attackProbabilities.Length; i++)
        {
            if (random >= attackProbabilities[i].minProbability && random <= attackProbabilities[i].maxProbability)
            {
                currentAction = attackProbabilities[i].nextAttack;
                anims.SetAnimationTrigger(attackProbabilities[i].animationTrigger);
                canRotate = false;
                break;
            }
        }

        evaluatingAttack = false;
    }
    IEnumerator BombaJaegerVFXTimer()
    {
        activateTargetVfx = true;
        yield return new WaitForSeconds(3.2f);
        activateTargetVfx = false;
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

            GorgonopsiaSFX.Instance.Play("rugidoExplosivo");
            if (stats.attackSpeedBonus) script.chargeTime = stats.rugidoExplosivoChargeTime - (stats.rugidoExplosivoChargeTime * stats.generalAttackSpeedBonus);
            else script.chargeTime = stats.rugidoExplosivoChargeTime;


            //if (currentHeatCharges!=2)StartCoroutine(TryToChargeHeat(stats.rugidoExplosivoChargeTime));
            //else StartCoroutine(ResetActing(stats.rugidoExplosivoChargeTime));
            StartCoroutine(ResetActing(stats.rugidoExplosivoChargeTime));
            //currentAction = Gstates.idle;
        }
        isActing = true;
    }
    public void HandleBombas360()
    {
        timer1 += Time.deltaTime;
        if (isActing == false)
        {

            //if (currentHeatCharges != 2) StartCoroutine(TryToChargeHeat(stats.bomba360Amount * stats.bomba360TimeToDamage));
            //else StartCoroutine(ResetActing(stats.bomba360Amount * stats.bomba360TimeToDamage));
            StartCoroutine(ResetActing(stats.bomba360Amount * stats.bomba360TimeToDamage));
        }

        isActing = true;
        for(int i = 0; i < stats.bomba360Amount; i++)
        {
            if (timer1 >= stats.bomba360TimeToDamage)
            {
                timer1 = 0;
                //GameObject obj = Instantiate(bombas360);
                //obj.transform.position = transform.position + new Vector3(0, 0.1f, 0);
                //Bomba360 script = obj.GetComponent<Bomba360>();
                //script.damage = stats.bombas360Damage;
                //if (stats.attackSpeedBonus) script.timeToDamage = stats.bomba360TimeToDamage - (stats.bomba360TimeToDamage * stats.generalAttackSpeedBonus);
                //else script.timeToDamage = stats.bomba360TimeToDamage;
                //script.distanceTreshold = stats.bomba360DistanceTreshold;

                GameObject obj = Instantiate(bombas360);
                obj.transform.position = transform.position+new Vector3(0,0.1f,0);
                Bombas360Version3 script = obj.GetComponent<Bombas360Version3>();
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
            isActing = true;
            canRotate = true;
            rotationSpeed = stats.rotationSpeedAliento;
            HandleBlink(TryGetEmbestidaPosition(stats.alientoBlinkDistance), stats.blinkDefaultTime);
            alientoCalor.SetActive(true);           
            StartCoroutine(ResetActing(stats.alientoCalorChargeTime + stats.alientoCalorDamageTime));
            
            Aliento2 script = alientoCalor.GetComponent<Aliento2>();

            script.alientoCalorChargeTime = stats.alientoCalorChargeTime;
            script.alientoCalorRange = stats.alientoCalorRange;
            script.alientoCalorAngle = stats.alientoCalorAngle;
            script.alientoCalorDamage = stats.alientoCalorDamage;
            script.alientoCalorDamageTime = stats.alientoCalorDamageTime;
            StartCoroutine(script.ChargeAliento());
            
        }
       
        
    }
    public void HandleBombaJaeger()
    {
        StartCoroutine(BombaJaegerVFXTimer());
        for (int i = 0; i < 2; i++)
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
            //obj.GetComponent<Rigidbody>().AddForce(transform.forward * 4, ForceMode.VelocityChange);
            BombaJaeger script = obj.GetComponent<BombaJaeger>();
            script.damage = stats.bombasJaegerDamage;
            if (stats.attackSpeedBonus) script.speed = stats.bombaJaegerSpeed + (stats.bombaJaegerSpeed * stats.generalAttackSpeedBonus);
            else script.speed = stats.bombaJaegerSpeed;

            script.distanceTreshHold = stats.bombaJaegerDistanceTreshHold;
            script.rotationSpeed = stats.bombaJaegerRotationSpeed;
            script.timeToAct = stats.bombaJaegerTimeToAct;
            script.trackingWindow = stats.trackingWindow;
        }

        //currentAction = Gstates.idle;
    }
    public void HandleBlink(Vector3 destination,float blinkTime)
    {

        foreach (SkinnedMeshRenderer renderer in meshes)
        {
            renderer.enabled = false;
        }
        //Mover al destino y rotar hacia el jugador
        transform.position = new Vector3(destination.x,originalPos.y,destination.z);
        var lookPos = player.transform.position;
        //lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
        //transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);

        audioSource.Play();
        //Activar renderer de objetos 
        StartCoroutine(ResetRenderer(blinkTime));
    }
    public void HandleEmbestidaFrenetica()
    {
        if (embestidaCounter >= stats.embestidaFreneticaAmount)
        {
            StartCoroutine(ResetActing(0));
            return;
        }
      

        if (bool1==true)timer1 += Time.deltaTime;

        isActing = true;

        //====================SET DAMAGES=======================
        foreach(EmbestidaFreneticaDamageTrigger obj in
            embestidaFreneticaParent.GetComponentsInChildren<EmbestidaFreneticaDamageTrigger>())
        {
            obj.damage = stats.embestidaFreneticaDamage;
        }

        //===================MOVE BOSS FORWARD====================
        
        //===============BLINK==============================
        if (bool2 == true)
        {                
            HandleBlink(TryGetEmbestidaPosition(stats.embestidaFreneticaBlinkDistance), stats.blinkDefaultTime);

            StartCoroutine(FeedbackEmbestida(stats.blinkDefaultTime));
            bool2 = false;
        }
        //========================MOTION=================================
        if (timer1 >= stats.embestidaFreneticaDelay+stats.blinkDefaultTime)
        {
            

            anims.SetAnimationTrigger("embestidaFrenetica");
            GorgonopsiaSFX.Instance.Play("embestida");
            timer1 = 0;
            bool1 = false;
            embestidaFreneticaParent.SetActive(true);
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position) * 2;
            //MOVE 
            float duration;
            if(stats.attackSpeedBonus) duration= stats.embestidaFreneticaDuration-(stats.embestidaFreneticaDuration*stats.generalAttackSpeedBonus);
            else duration= stats.embestidaFreneticaDuration;

                
            embestidaTween = transform.DOMove(transform.position+(transform.forward*distanceToPlayer),
                duration,false).SetEase(Ease.Linear).OnComplete(ResetEmbestidaParentColliders);
            embestidaTween.Play();
        }
    }
    public IEnumerator FeedbackEmbestida(float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 feedback = embestidaFeedback.transform.localScale;
        float feedbackDuration = stats.embestidaFreneticaDelay + stats.blinkDefaultTime;
        float feedbackLenght = Vector3.Distance(transform.position, player.transform.position) / 10 * 2;

        embestidaFeedback.transform.DOScale(new Vector3(feedback.x, feedback.y, feedbackLenght * 2), feedbackDuration);
    }
    public Vector3 TryGetEmbestidaPosition(float distance)
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

           

            Vector3 desiredPosition = new Vector3(playerPos.x + (distance * xRandom)
            , transform.position.y, playerPos.z + (distance * zRandom));

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
        anims.SetAnimationTrigger("embestidaEnd");
        embestidaFreneticaParent.SetActive(false);
        embestidaFeedback.transform.localScale = new Vector3(embestidaFeedback.transform.localScale.x,
                    embestidaFeedback.transform.localScale.y, 0.000001f);
        bool1 = true;
        bool2 = true;
        embestidaCounter++;
        

    }
    public void DeactivateEmbestidaFeedback() => embestidaFeedback.transform.localScale = new Vector3(embestidaFeedback.transform.localScale.x,
                    embestidaFeedback.transform.localScale.y, 0);
   
    public IEnumerator DeactivateLegColliders(float time)
    {
        yield return new WaitForSeconds(time);
        legParent.SetActive(false);
    }
    public void UpdateCargaCalorVisuals()
    {

        switch (currentHeatCharges)
        {
            case 1:
                stats.fireBonus = true;
                stats.attackSpeedBonus = false;
                cargaCalor[0].SetActive(true);
                break;

            case 2:
                stats.fireBonus = true;
                stats.attackSpeedBonus = true;
                cargaCalor[1].SetActive(true);
                break;

            default:
                stats.fireBonus = false;
                stats.attackSpeedBonus = false;
                cargaCalor[0].SetActive(false);
                cargaCalor[1].SetActive(false);
                break;
        }
        float currentHealth = stats.health / stats.maxHP;


        if (currentHealth <= stats.heatTreshhold[0])
        {
            currentHeatCharges = 2;           
            return;
        }
        else if (currentHealth <= stats.heatTreshhold[1])
        {
            currentHeatCharges = 1;            
            return;
        }
        else
        {
            currentHeatCharges = 0;            
            return;
        }      
    }
    public void LegCheck()
    {
        if (currentAction == Gstates.cargaCalor&&isActing)
        {
            if (leftLegOn == false && rightLegOn == false)
            {
                currentAction = Gstates.idle;
                isActing = false;
                StopAllCoroutines();
            }
        }
    }
    public void HandleDeath()
    {
        stats.isAlive = false;
        ProgressManager progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        progress.enemysdeath = 1000;
        BuffManager.onBossDefetead?.Invoke();
    }
    //========================UTILITY METHODS========================
    private void RotateToPlayer()
    {
        Quaternion direction = Quaternion.LookRotation(player.transform.position - transform.position);
        direction.x = 0;

        if (canRotate==true)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction,
            rotationSpeed * Time.deltaTime);
        }

    }
    public IEnumerator ResetActing(float time)
    {
        yield return new WaitForSeconds(time);
        //attacked = false;
        currentAction = Gstates.idle;
        timer1 = 0;
        isActing = false;
        embestidaCounter = 0;
        canRotate = true;
        rotationSpeed = stats.defaultRotationSpeed;
    }

    public IEnumerator ResetRenderer(float time)
    {
        yield return new WaitForSeconds(time);
        foreach (SkinnedMeshRenderer renderer in meshes)
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
        if (other.CompareTag("PlayerWeapon")&&stats.isActive)
        {
            hitSound.Play();
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
            music.Play();
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
    public string animationTrigger;
}

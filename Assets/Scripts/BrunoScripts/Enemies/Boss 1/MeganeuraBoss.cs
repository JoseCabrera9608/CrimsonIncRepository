using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MeganeuraBoss : MonoBehaviour
{
    [SerializeField] public Maction currentAction;
    [SerializeField] private MeganeuraAnims anims;
    private MeganeuraStats stats;
    private Dictionary<Maction, float> damages;

    public GameObject EnemyBar;

    private ProgressManager progress;

    public GameObject stake;
    public List<GameObject> stakeList;
    public Transform[] posTransform;
    private Vector3[] pos = new Vector3[2];
    Tween bobing;

    private GameObject player;
    public PlayerStatus playerStatus;
    private Dictionary<int, Maction> actionsDic = new Dictionary<int, Maction>()
    {
        {0,Maction.rayoIon },
        {1,Maction.bombardeoMisiles },
        {2,Maction.misilesEmp },
        {3,Maction.bombaFlash },
        {4,Maction.lluviaLasers },
        {5,Maction.vistaCazador },
        {6,Maction.discosEmp },
    };


    //Multi vars
    [HideInInspector] public float currentDamageValue;
    private bool evaluating = false;
    private float t1;
    private int container1 = 0;
    private bool bool1 = false;
    [SerializeField] private Transform attackPos;
    //Spawn objects
    [Header("Objetos")]
    [SerializeField] private GameObject rayoEmp;
    [SerializeField] private GameObject explotion;
    [SerializeField] private GameObject lluviaDeLasers;
    [SerializeField] private GameObject bombaFlash;
    [SerializeField] private GameObject discoEmp;
    [SerializeField] private GameObject rayoIon;
    [SerializeField] private GameObject vistaCazador;

    /*[HideInInspector]*/
    public bool isActive = false;
    [SerializeField] private float timeToActivate;
    [SerializeField] private BoxCollider col;
    private bool initialStakes = false;



    [Header("=====Attack probabilities=======")]
    public ProbabilitiesOnAir[] airAttacks;
    public ProbabilitiesOnGround[] groundAttacks;

    [Header("=====AUTORIZACION DE ATAQUES DEL PROTOCOLO DE LOS DEPREDADORES=======")]
    public bool attackOnGround;

    [Header("VERSION ARENA")]
    public bool dummy;
    public bool attackOnCooldown;

    private void OnEnable()
    {
        PlayerStatus.onPlayerDeath += SuperReset;
    }
    private void OnDisable()
    {
        PlayerStatus.onPlayerDeath -= SuperReset;
    }
    private void Start()
    {
        stats = GetComponent<MeganeuraStats>();
        currentAction = Maction.idle;
        //anims.GetComponent<MeganeuraAnims>();
        damages = stats.attacksDamage;
        player = FindObjectOfType<PlayerStatus>().gameObject;
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        bobing.Pause();
        SetPos();

        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }
    private void Update()
    {

        if (stats.isAlive&&isActive&&player!=null)
        {
            StakesCheck();
            RotateToPlayer();
            StateMachine();
        }

        if (playerStatus.playerdeath == true)
        {
            Deactivate();
        }

        if (stats.isAlive == false)
        {
            
            if (dummy == false)
            {
                FindObjectOfType<AudioManager>().Stop("MusicaBoss");
            }

            progress.enemysdeath = 100;
            StopAllCoroutines();
            bobing.Kill();
        }
        //ARENA
        if (dummy&&attackOnCooldown==false) Arena();
    }

    private void StateMachine()
    {
        //if (currentAction == Action.idle)
        //{
        //    StartCoroutine(EvaluateAttack());
        //}
        switch (currentAction)
        {
            case Maction.idle:
                if (evaluating == false /*arena*/&& dummy == false) StartCoroutine(EvaluateAttack());
                else attackOnCooldown = false;
                break;

            case Maction.rayoIon:
                HandleRayoIon();
                //currentDamageValue = damages[Action.rayoIon];
                break;

            case Maction.bombardeoMisiles:
                HandleBombardeoMisiles();
                break;

            case Maction.misilesEmp:
                HandleRayosEmp();
                break;

            case Maction.bombaFlash:
                HandleBombaFlash();
                break;
            case Maction.lluviaLasers:
                HandleLluviaDeLasers();
                break;

            case Maction.vistaCazador:
                HandleVistaCazador();
                //currentDamageValue = damages[Action.vistaCazador];
                break;

            case Maction.discosEmp:
                HandleDiscosEmp();
                break;
            
        }
        
    }
    private void RotateToPlayer()
    {
        Quaternion direction = Quaternion.LookRotation(player.transform.position - transform.position);
        direction.x = 0;

        if (stats.canRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction,
            stats.rotationSpeed * Time.deltaTime);
        }
        
    }
    public void GravityStake()
    {
        Vector3 direction;
        direction.y = Random.Range(0, 361);
        direction.x = 90;
        direction.z = 0;
        for (int i = 0; i < stats.stakesToThrow; i++)
        {           
            GameObject _stake = Instantiate(stake);
            direction.y += 90;
            _stake.transform.position = transform.position;
            _stake.transform.localEulerAngles = direction;
            _stake.transform.DORotate(new Vector3(180, _stake.transform.localEulerAngles.y, _stake.transform.localEulerAngles.z), stats.stakeRotationDuration).SetEase(Ease.InQuint);
            _stake.GetComponent<Rigidbody>().velocity = _stake.transform.up*stats.stakeSpeed;
            _stake.GetComponent<Stake>().parent = transform;
            
            //_stake.transform.parent = transform;
            stakeList.Add(_stake);
        }
        stats.onAir = true;
    }
    private void StakesCheck()
    {
        if (stakeList.Count >= stats.stakesToThrow&&stats.onAir==false)
        {
            stats.onAir = true;
        }
        //Contar cuantas estacas quedan en la lista
        if (stats.onAir)
        {   
            //Remover en la lista los elementos nulos/destuidos
            for(int i = 0; i < stakeList.Count; i++)
            {
                if (stakeList[i] == null) stakeList.RemoveAt(i);
            }

            if (stakeList.Count == 0)
            {
                FlightSwitch( false);
                StartCoroutine(SpawnStakes());
            }
        }else if (stats.onAir == false && initialStakes == false)
        {
            initialStakes = true;
            StartCoroutine(SpawnStakes());
        }


        for(int i = 0; i < stakeList.Count; i++)
        {
            if (i > stats.stakesToThrow - 1) Destroy(stakeList[i]);
        }
    }
    private IEnumerator SpawnStakes()
    {
        yield return new WaitForSeconds(stats.onGroundTime);
        GravityStake();
        anims.SetTrigger("estacas");
        FlightSwitch(true);
        stats.canRotate = true;
    }

    //UTILITY
    private void StartBobing()
    {
        bobing = transform.DOMove(pos[1] + new Vector3(0, -2, 0), 7, false)
            .SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo);
        bobing.Play();

        stats.onAir = true;
    }
    private void StopBobing()
    {
        bobing.Pause();
        stats.onAir = false;
    }
    private void FlightSwitch(bool changeOnAir)
    {
        if (changeOnAir)
        {
            stats.canRotate = true;
            transform.DOMove(pos[1], stats.startFlightDuration, false).SetEase(Ease.OutBack).OnComplete(StartBobing);
        }
        else
        {
            currentAction = Maction.idle;
            stats.isAttacking = false;
            stats.canRotate = false;
            transform.DORotate(new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z),
                stats.descendDuration*.6f,RotateMode.Fast);

            transform.DOMove(pos[0], stats.descendDuration, false).SetEase(Ease.InBack).OnComplete(StopBobing);
        }

        stats.onAir = changeOnAir;
    }
    private void SetPos()
    {
        for(int i = 0; i < posTransform.Length; i++)
        {
            pos[i] = posTransform[i].position;
        }
    }
    private void CheckHealth()
    {
        if (stats.health <= 0&&stats.isAlive)
        {
            stats.isAlive = false;
            HandleDeath();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon")&&isActive)
        {
            stats.health -= PlayerSingleton.Instance.playerDamage;
            if(stats.isAlive)anims.DamageAnimation();
            CheckHealth();
        }
        if (other.CompareTag("Player")&&col.enabled==true)
        {            
            StartCoroutine(Activate());
        }

    }

    private IEnumerator Activate()
    {
        col.enabled = false;
        //quitar luego
        if (dummy == false)
        {
            FindObjectOfType<AudioManager>().Play("MusicaBoss");
        }
        if (EnemyBar!=null)EnemyBar.SetActive(true);
        yield return new WaitForSeconds(timeToActivate);
        isActive = true;
        
    }

    public void Deactivate()
    {
        col.enabled = true;
        EnemyBar.SetActive(false);
       
        isActive = false;
    }

    //HANDLE ATTACK METHODS
    private IEnumerator EvaluateAttack()
    {
        evaluating = true;
        yield return new WaitForSeconds(stats.attackDelay);

        int random=Random.Range(0,101);

        if (stats.onAir)
        {
            for(int i = 0; i < airAttacks.Length; i++)
            {
                if (random >= airAttacks[i].minProbability && random <= airAttacks[i].maxProbability)
                {
                    currentAction = airAttacks[i].nextAttack;
                    anims.SetTrigger(airAttacks[i].animationTriggerName);
                    break;
                }
            }

        }
        else if(stats.onAir==false&&attackOnGround)
        {
            //random = Random.Range(2, 5);
            for (int i = 0; i < groundAttacks.Length; i++)
            {
                if (random >= groundAttacks[i].minProbability && random <= groundAttacks[i].maxProbability)
                {
                    currentAction = groundAttacks[i].nextAttack;
                    anims.SetTrigger(groundAttacks[i].animationTriggerName);
                    break;
                }
            }
        }
        
        //currentAction = actionsDic[random];
        evaluating = false;
        currentDamageValue = damages[currentAction];
    }

    //done
    private void HandleRayoIon()
    {
        //Debug.Log("Handling: " + currentAction);
        //stats.isAttacking = false;
        //currentAction = Action.idle;

        t1 += Time.deltaTime;
        stats.isAttacking = true;
        rayoIon.SetActive(true);
        stats.canRotate = false;

        MeganeuraLaser laser = rayoIon.GetComponent<MeganeuraLaser>();
        laser.damage = currentDamageValue;
        laser.rotationSpeed = stats.laserRotationSpeed;
        laser.damagePerTick = true;

        if (t1 > stats.laserAimTime*0.8f&&t1<stats.laserAimTime)
        {
            laser.canRotate = false;
        }else if (t1 > stats.laserAimTime)
        {
            laser.isActive = true;
            laser.canRotate = true;
        }

        if (t1 >= stats.laserAimTime + stats.laserAttackTime)
        {
            rayoIon.SetActive(false);
            currentAction = Maction.idle;
            stats.isAttacking = false;
            t1 = 0;
            stats.canRotate = true;
        }
    }
    //done
    private void HandleBombardeoMisiles()
    {
        t1 += Time.deltaTime;
        bool1 = true;
        stats.isAttacking = true;

        if (t1 > stats.bmDelay&&container1<stats.bmAmount)
        {
            container1++;
            t1 = 0;
            GameObject _explotion = Instantiate(explotion);
            _explotion.transform.position = player.transform.position;
            _explotion.GetComponent<Misiles>().damage = damages[Maction.bombardeoMisiles];
            _explotion.GetComponent<Misiles>().timer = stats.bmTimeToExplode;
        }else if (container1 >= stats.bmAmount && bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            stats.isAttacking = false;
            currentAction = Maction.idle;               
        }
    }
    //done
    private void HandleRayosEmp()
    {
        bool1 = true;
        stats.isAttacking = true;
        t1 += Time.deltaTime;       
        if (t1 > 1&&container1<stats.rempAmount)
        {
            t1 = 0;
            container1++;
            GameObject misile = Instantiate(rayoEmp);
            misile.transform.position = attackPos.position+attackPos.forward*2;
            misile.transform.localEulerAngles = new Vector3(Random.Range(0,-46), Random.Range(-90, 91), 0);
            misile.GetComponent<RayoEmp>().damage = damages[Maction.misilesEmp];               
            misile.GetComponent<RayoEmp>().speed = stats.rempSpeed;               
            misile.GetComponent<RayoEmp>().rotationSpeed = stats.rempRotationSpeed;               
            misile.GetComponent<RayoEmp>().lifeTime = stats.rempLifeTime;               
            misile.GetComponent<RayoEmp>().followTime = stats.rempFollowTime;               
        }else if (container1 >= 5&&bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            stats.isAttacking = false;
            currentAction = Maction.idle;
        }
    }
    //done
    private void HandleBombaFlash()
    {
        stats.isAttacking = true;

        GameObject bomba = Instantiate(bombaFlash);
        bomba.transform.position = attackPos.position+bomba.transform.forward;
        bomba.GetComponent<BombaFlash>().delay = stats.flashDelay;
        bomba.GetComponent<BombaFlash>().flashDuration = stats.flashDuration;

        stats.isAttacking = false;
        currentAction = Maction.idle;
    }
    //done
    private void HandleLluviaDeLasers()
    {
        t1 += Time.deltaTime;
        bool1 = true;
        stats.isAttacking = true;

        if (t1 > stats.lluviaDelay && container1 < stats.lluviaAmount)
        {
            container1++;
            t1 = 0;
            GameObject obj = Instantiate(lluviaDeLasers);
            obj.transform.position = player.transform.position + new Vector3(0, stats.lluviaLasersHeight, 0);
            obj.GetComponent<LluviaLasers>().damage = damages[Maction.lluviaLasers];
        }
        else if (container1 >= stats.lluviaAmount && bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            stats.isAttacking = false;
            currentAction = Maction.idle;
        }

    }

    private void HandleVistaCazador()
    {
        t1 += Time.deltaTime;
        stats.isAttacking = true;
        vistaCazador.SetActive(true);
        stats.canRotate = false;

        MeganeuraLaser laser = vistaCazador.GetComponent<MeganeuraLaser>();
        laser.damage = currentDamageValue;
        laser.rotationSpeed = stats.laserRotationSpeed;
        laser.damagePerTick = false;

        if (t1 > stats.cazadorAimTime * 0.8f && t1 < stats.cazadorAimTime)
        {
            laser.canRotate = false;
        }
        else if (t1 > stats.cazadorAimTime)
        {
            laser.isActive = true;
        }

        if (t1 >= stats.cazadorAimTime + stats.cazadorLaserDuration)
        {
            vistaCazador.SetActive(false);
            currentAction = Maction.idle;
            stats.isAttacking = false;
            t1 = 0;
            stats.canRotate = true;
        }
    }
    //done
    private void HandleDiscosEmp()
    {
        t1 += Time.deltaTime;
        stats.isAttacking = true;
        if (t1 > stats.discDelay&&container1<stats.discAmount)
        {
            t1 = 0;
            container1++;
            GameObject disc = Instantiate(discoEmp);
            disc.transform.position = attackPos.position + attackPos.forward;
            disc.GetComponent<DiscosEmp>().damage = damages[Maction.discosEmp];
            disc.GetComponent<DiscosEmp>().extraFollowTime = stats.empDiscExtraFollowTime;
            disc.GetComponent<DiscosEmp>().staminaDamage = stats.discosEmpStaminaLoss;
            disc.GetComponent<DiscosEmp>().maxSpeed = stats.discMaxSpeed;
            disc.GetComponent<DiscosEmp>().rotationSpeed = stats.discRotationSpeed;
            disc.GetComponent<DiscosEmp>().left = bool1;
            bool1 = !bool1;

        }else if (container1 >= stats.discAmount)
        {
            t1 = 0;
            container1=0;
            bool1 = false;
            currentAction = Maction.idle;
            stats.isAttacking = false;
        }

    }

    private void HandleDeath()
    {
        //logic to handle death
        FlightSwitch(false);
        foreach(GameObject stake in stakeList)
        {
            Destroy(stake);
        }
        StopBobing();
        StopAllCoroutines();
        BuffManager.onBossDefetead?.Invoke();
    }

    private void Arena()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            attackOnCooldown = true;
            currentAction = Maction.rayoIon;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            attackOnCooldown = true;
            currentAction = Maction.bombardeoMisiles;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            attackOnCooldown = true;
            currentAction = Maction.misilesEmp;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            attackOnCooldown = true;
            currentAction = Maction.bombaFlash;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            attackOnCooldown = true;
            currentAction = Maction.lluviaLasers;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            attackOnCooldown = true;
            currentAction = Maction.vistaCazador;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            attackOnCooldown = true;
            currentAction = Maction.discosEmp;
        }
    }

    [ContextMenu("Super Reset olle zhy")]
    public void SuperReset()
    {
        foreach(GameObject stake in stakeList)
        {
            Destroy(stake);
        }
        GameObject obj = Instantiate(stats.nuera);
        obj.transform.position = stats.initialPos;
        obj.transform.localEulerAngles = stats.initialRot;
        obj.GetComponent<MeganeuraBoss>().isActive = false;
        obj.GetComponent<MeganeuraBoss>().col.enabled = true;
        obj.GetComponent<MeganeuraBoss>().stakeList.Clear();
        
        FindObjectOfType<AudioManager>().Stop("MusicaBoss");
        Destroy(gameObject);
    }
}
public enum Maction
{
    idle,                   
    rayoIon, //on air
    bombardeoMisiles, // air grond
    misilesEmp, //air ground
    bombaFlash, //air ground
    lluviaLasers, //on air
    vistaCazador, // on air
    discosEmp //on air
};
[System.Serializable]
public class ProbabilitiesOnAir
{
    [SerializeField] public Maction nextAttack;
    public float minProbability;
    public float maxProbability;
    public string animationTriggerName;
}
[System.Serializable]
public class ProbabilitiesOnGround
{
    [SerializeField] public Maction nextAttack;
    public float minProbability;
    public float maxProbability;
    public string animationTriggerName;
}

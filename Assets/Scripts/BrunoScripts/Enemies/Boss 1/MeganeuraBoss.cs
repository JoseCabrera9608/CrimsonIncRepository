using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MeganeuraBoss : MonoBehaviour
{
    [SerializeField]public Action currentAction;
    private MeganeuraStats stats;
    private Dictionary<Action,float> damages;

    public GameObject EnemyBar;


    public GameObject stake;
    public List<GameObject> stakeList;
    public Transform[] posTransform;
    private Vector3[] pos=new Vector3[2];
    Tween bobing;

    private GameObject player;
    private Dictionary<int, Action> actionsDic=new Dictionary<int, Action>()
    {
        {0,Action.rayoIon },
        {1,Action.bombardeoMisiles },
        {2,Action.rayosEmp },
        {3,Action.bombaFlash },
        {4,Action.lluviaLasers },
        {5,Action.vistaCazador },
        {6,Action.discosEmp },
    };


    //Multi vars
    [HideInInspector] public float currentDamageValue;
    private bool evaluating = false;
    private float t1;
    private int container1=0;
    private bool bool1=false;
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

    [SerializeField]private bool isActive=false;
    [SerializeField] private float timeToActivate;
    [SerializeField]private BoxCollider col;
    private bool initialStakes=false;
    private void Start()
    {
        stats = GetComponent<MeganeuraStats>();
        damages = stats.attacksDamage;
        player = FindObjectOfType<PlayerStatus>().gameObject;
        bobing.Pause();
        SetPos();
    }
    private void Update()
    {

        if (stats.isAlive&&isActive&&player!=null)
        {
            StakesCheck();
            RotateToPlayer();
            StateMachine();
        }
        
    }

    private void StateMachine()
    {
        //if (currentAction == Action.idle)
        //{
        //    StartCoroutine(EvaluateAttack());
        //}
        switch (currentAction)
        {
            case Action.idle:
                if (evaluating == false) StartCoroutine(EvaluateAttack());
                break;

            case Action.rayoIon:
                HandleRayoIon();
                //currentDamageValue = damages[Action.rayoIon];
                break;

            case Action.bombardeoMisiles:
                HandleBombardeoMisiles();
                break;

            case Action.rayosEmp:
                HandleRayosEmp();
                break;

            case Action.bombaFlash:
                HandleBombaFlash();
                break;
            case Action.lluviaLasers:
                HandleLluviaDeLasers();
                break;

            case Action.vistaCazador:
                HandleVistaCazador();
                //currentDamageValue = damages[Action.vistaCazador];
                break;

            case Action.discosEmp:
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
    private void GravityStake()
    {
        Vector3 direction;
        
        for(int i = 0; i < stats.stakesToThrow; i++)
        {
            direction.y = Random.Range(0,361);
            direction.x = 95;
            direction.z = 0;

            GameObject _stake = Instantiate(stake);
            _stake.transform.position = transform.position;
            _stake.transform.localEulerAngles = direction;
            _stake.GetComponent<Rigidbody>().velocity = _stake.transform.up*50;
            
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
    }
    private IEnumerator SpawnStakes()
    {
        yield return new WaitForSeconds(stats.onGroundTime);
        GravityStake();
        FlightSwitch(true);
        stats.canRotate = true;
    }

    //UTILITY
    private void StartBobing()
    {
        bobing = transform.DOMove(pos[1] + new Vector3(0, -2, 0), 7, false)
            .SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo);
        bobing.Play();
    }
    private void StopBobing() => bobing.Pause();
    private void FlightSwitch(bool changeOnAir)
    {
        if (changeOnAir)
        {
            stats.canRotate = true;
            transform.DOMove(pos[1], 7, false).SetEase(Ease.OutBack).OnComplete(StartBobing);
        }
        else
        {
            stats.canRotate = false;
            transform.DOMove(pos[0], 3, false).SetEase(Ease.InBack).OnComplete(StopBobing);
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
        if (other.CompareTag("PlayerWeapon"))
        {
            stats.health -= PlayerSingleton.Instance.playerDamage;
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
        EnemyBar.SetActive(true);
        yield return new WaitForSeconds(timeToActivate);
        isActive = true;
        
    }
    //HANDLE ATTACK METHODS
    private IEnumerator EvaluateAttack()
    {
        evaluating = true;
        yield return new WaitForSeconds(stats.attackDelay);

        int random;

        if (stats.onAir)
        {
            random = Random.Range(0, 7);
        }
        else
        {
            random = Random.Range(2, 5);
        }
        
        currentAction = actionsDic[random];
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
            currentAction = Action.idle;
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
            _explotion.GetComponent<Misiles>().damage = damages[Action.bombardeoMisiles];
        }else if (container1 >= stats.bmAmount && bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            stats.isAttacking = false;
            currentAction = Action.idle;               
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
            misile.GetComponent<RayoEmp>().damage = damages[Action.rayosEmp];               
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
            currentAction = Action.idle;
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
        currentAction = Action.idle;
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
            obj.transform.position = player.transform.position + new Vector3(0, 10, 0);
            obj.GetComponent<LluviaLasers>().damage = damages[Action.lluviaLasers];
        }
        else if (container1 >= stats.lluviaAmount && bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            stats.isAttacking = false;
            currentAction = Action.idle;
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
            currentAction = Action.idle;
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
            disc.GetComponent<DiscosEmp>().damage = damages[Action.discosEmp];
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
            currentAction = Action.idle;
            stats.isAttacking = false;
        }

    }

    private void HandleDeath()
    {
        //logic to handle death
        FlightSwitch(false);
        StopAllCoroutines();
        BuffManager.Instance.ShowPanel();
    }
}
public enum Action
{
    idle,                   
    rayoIon, //on air
    bombardeoMisiles, //both
    rayosEmp, //both
    bombaFlash, //both
    lluviaLasers, //on air
    vistaCazador, // on air
    discosEmp //on air
};

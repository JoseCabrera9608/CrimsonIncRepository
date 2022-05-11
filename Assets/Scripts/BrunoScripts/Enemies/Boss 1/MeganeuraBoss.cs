using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MeganeuraBoss : MonoBehaviour
{
    [SerializeField]public Action currentAction;
    private MeganeuraStats stats;
    private Dictionary<Action,float> damages;

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
        {4,Action.bombaNapalm },
        {5,Action.vistaCazador },
        {6,Action.discosEmp },
    };

    //Multi vars
    [SerializeField] public float currentDamageValue;
    [SerializeField] private bool evaluating = false;
    [SerializeField] private float t1;
    [SerializeField] private int container1=0;
    [SerializeField] private bool bool1=false;
    [SerializeField] private Transform attackPos;
    private LineRenderer lr;
    private Ray attackRay;

    //Spawn objects
    [Header("Objectos")]
    [SerializeField] private GameObject rayoEmp;
    [SerializeField] private GameObject explotion;
    [SerializeField] private GameObject bombaNapalm;
    private void Start()
    {
        stats = GetComponent<MeganeuraStats>();
        damages = stats.attacksDamage;
        player = FindObjectOfType<PlayerStatus>().gameObject;
        lr = GetComponent<LineRenderer>();
        bobing.Pause();

        StartCoroutine(SpawnStakes());
        SetPos();
    }
    private void Update()
    {
        //Test
        //if (Input.GetMouseButtonDown(0)) GravityStake();
        //if (Input.GetKeyDown(KeyCode.P)) EvaluateAttack();

        StakesCheck();
        RotateToPlayer();
        StateMachine();
    }

    private void StateMachine()
    {       
        if (stats.onAir)
        {
            switch (currentAction)
            {
                case Action.idle:
                    if(evaluating==false) StartCoroutine(EvaluateAttack());
                    break;

                case Action.rayoIon:
                    HandleRayoIon(3,4);
                    currentDamageValue = stats.attacksDamage[currentAction];

                    break;

                case Action.bombardeoMisiles:
                    HandleBombardeoMisiles(0.5f);
                    break;

                case Action.rayosEmp:
                    HandleRayosEmp(5);
                    break;

                case Action.bombaFlash:
                    HandleBombaFlash();
                    break;
                case Action.bombaNapalm:
                    HandleBombaNapalm(3,1);
                    break;

                case Action.vistaCazador:
                    HandleVistaCazador();
                    break;

                case Action.discosEmp:
                    HandleDiscosEmp();
                    break;
            }
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
        if (changeOnAir)transform.DOMove(pos[1], 7, false).SetEase(Ease.OutBack).OnComplete(StartBobing);
        else transform.DOMove(pos[0], 3, false).SetEase(Ease.InBack).OnComplete(StopBobing);

        stats.onAir = changeOnAir;
    }
    private void SetPos()
    {
        for(int i = 0; i < posTransform.Length; i++)
        {
            pos[i] = posTransform[i].position;
        }
    }

    //HANDLE ATTACK METHODS
    private IEnumerator EvaluateAttack()
    {
        evaluating = true;
        yield return new WaitForSeconds(stats.attackDelay);

        int random;
        random =Random.Range(0, 7);
        currentAction = actionsDic[random];
        evaluating = false;
        currentDamageValue = damages[currentAction];
    }

    private void HandleRayoIon(float aimTime,float laserDuration)
    {
        stats.isAttacking = true;
        lr.enabled = true;
        t1 += Time.deltaTime;
        bool1 = false;
        stats.canRotate = false;
        BoxCollider col = attackPos.GetComponent<BoxCollider>();
        col.enabled = false;

        attackRay.origin = attackPos.position;
        attackRay.direction = attackPos.forward;

        Quaternion direction = Quaternion.LookRotation(player.transform.position - attackPos.position);

        if(t1<aimTime*0.85f&&bool1==false)
            attackPos.rotation = Quaternion.RotateTowards(attackPos.rotation, direction, 7 * Time.deltaTime);
        else if (t1 > aimTime)
        {
            bool1 = true;
            attackPos.rotation = Quaternion.RotateTowards(attackPos.rotation, direction, 10 * Time.deltaTime);
            lr.startWidth=1.5f;
            lr.endWidth=1.5f;

            col.enabled = true;
        }
        RaycastHit hit;
        if (Physics.Raycast(attackRay,out hit, 100))
        {
            lr.SetPosition(0, attackPos.position);
            lr.SetPosition(1, hit.point);
        }

        if (t1 > aimTime + laserDuration)
        {
            lr.enabled = false;
            lr.startWidth = 0.2f;
            lr.endWidth = 0.2f;
            t1 = 0;
            col.enabled = false;
            stats.canRotate = true;
            currentAction = Action.idle;
            bool1 = false;
        }

        
    }
    private void HandleBombardeoMisiles(float delay)
    {
        t1 += Time.deltaTime;
        bool1 = true;
        stats.isAttacking = true;

        if (t1 > delay&&container1<30)
        {
            container1++;
            t1 = 0;
            GameObject _explotion = Instantiate(explotion);
            _explotion.transform.position = player.transform.position;
            _explotion.GetComponent<Misiles>().damage = damages[Action.bombardeoMisiles];
        }else if (container1 >= 30 && bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            stats.isAttacking = false;
            currentAction = Action.idle;               
        }
    }
    private void HandleRayosEmp(int emp)
    {
        bool1 = true;
        stats.isAttacking = true;
        t1 += Time.deltaTime;       
        if (t1 > 1&&container1<emp)
        {
            t1 = 0;
            container1++;
            GameObject misile = Instantiate(rayoEmp);
            misile.transform.position = attackPos.position+new Vector3(0,0,2);
            misile.transform.localEulerAngles = new Vector3(Random.Range(0,-46), Random.Range(-90, 91), 0);
            misile.GetComponent<RayoEmp>().damage = damages[Action.rayosEmp];               
        }else if (container1 >= 5&&bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            stats.isAttacking = false;
            currentAction = Action.idle;
        }
    }
    private void HandleBombaFlash()
    {
        Debug.Log("Handling: " + currentAction);
        currentAction = Action.idle;
        stats.isAttacking = false;
    }
    private void HandleBombaNapalm(int bombs,float delay)
    {
        //Debug.Log("Handling: " + currentAction);
        //stats.isAttacking = false;
        //currentAction = Action.idle;
        t1 += Time.deltaTime;
        bool1 = true;
        stats.isAttacking = true;

        if (t1 > delay && container1 < bombs)
        {
            container1++;
            t1 = 0;
            GameObject napalm = Instantiate(bombaNapalm);
            napalm.transform.position = attackPos.position;
            napalm.GetComponent<BombaNapalm>().explotionDamage = damages[Action.bombaNapalm];
            napalm.GetComponent<BombaNapalm>().burnDamage = stats.bombaNapalmBurnDamage;
        }
        else if (container1 >= bombs && bool1)
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
        Debug.Log("Handling: " + currentAction);
        stats.isAttacking = false;
        currentAction = Action.idle;
    }
    private void HandleDiscosEmp()
    {
        Debug.Log("Handling: " + currentAction);
        stats.isAttacking = false;
        currentAction = Action.idle;


    }
}
public enum Action
{
    idle,
    rayoIon,
    bombardeoMisiles,
    rayosEmp,
    bombaFlash,
    bombaNapalm,
    vistaCazador,
    discosEmp
};

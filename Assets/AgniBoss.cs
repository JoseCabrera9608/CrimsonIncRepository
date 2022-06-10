using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Agtion
{
    idle,
    rayoPalma, //on air
    bombardeo, // air grond
    estacasStun //air ground
    //bombaFlash, //air ground
    //lluviaLasers, //on air
    //vistaCazador, // on air
    //discosEmp //on air
};

[System.Serializable]
public class AgniProbabilities
{
    [SerializeField] public Agtion nextAttack;
    public float minProbability;
    public float maxProbability;
    public string animationTriggerName;
}

public class AgniBoss : MonoBehaviour
{
    [SerializeField] public Agtion currentAction;
    public GameObject player;

    public float closeDist;
    public float mediumDist;
    public float farDist;
    public float dist;

    public GameObject bombaboom;
    public GameObject bombaIce;
    public List<GameObject> bombList;

    public int bombNumber;
    public float bombspeed;
    public float bombRotationTime;

    public Vector3 direction2;
    public GameObject bombSpawn;

    public bool modoNitrogeno;

    
    //Rasho Laser
    [SerializeField] private GameObject vistaCazador;
    public MeganeuraLaser laser;
    
    public float t1;
    public float cazadorLaserDuration;
    public float cazadorAimTime;
    public float laserRotationSpeed;
    public float currentDamageValue;
    public bool disparado;
    public bool rayoenrango;

    //Estacas Stun

    [Header("Estacas Stun")]

    [SerializeField] private GameObject lluviaDeLasers;
    public int lluviaAmount;
    public float lluviaDelay;
    public float lluviaLasersHeight;
    private int container1 = 0;
    private bool bool1 = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        laser = vistaCazador.GetComponent<MeganeuraLaser>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bombList.Count; i++)
        {
            if (bombList[i] == null) bombList.RemoveAt(i);
        }

        if (bombList.Count == 0)
        {
            //Bombas();
            //HandleVistaCazador();
        }
    }
    private void FixedUpdate()
    {
        Iddle();
        RayoPalma();

    }

    public void Iddle()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);
        
        if (dist <= closeDist)
        {
            PreparacionBombas();
        }
        if (dist <= mediumDist && dist > closeDist)
        {

            PreparacionRayo();
        }
        else
        {
            //laser.isActive = false;
            rayoenrango = false;
            vistaCazador.SetActive(false);
        }
        if (dist > mediumDist)
        {
            HandleLluviaDeLasers();
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
            case Agtion.idle:
                if (evaluating == false /*arena*/&& dummy == false) StartCoroutine(EvaluateAttack());
                else attackOnCooldown = false;
                break;

            case Agtion.rayoPalma:
                HandleRayoIon();
                //currentDamageValue = damages[Action.rayoIon];
                break;

            case Agtion.bombardeo:
                HandleBombardeoMisiles();
                break;

            case Agtion.estacasStun:
                HandleRayosEmp();
                break;

            /*case Maction.bombaFlash:
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
                break;*/

        }

    }

    public void PreparacionBombas()
    {
        if (bombList.Count == 0)
        {
            if (bombspeed > 20)
            {
                bombspeed = 10;
            }

            Bombas();
            bombspeed += 5;
        }
    }

    public void Bombas()
    {
        Vector3 direction;
        direction.y = 0;
        direction.x = 90;
        direction.z = Random.Range(0, 361);
        for (int i = 0; i < bombNumber; i++) //cambiar el 8 por variable
        {
            if (modoNitrogeno)
            {
                GameObject bomb = Instantiate(bombaIce);
                //GameObject bomb = Instantiate(bombaboom);
                direction.y += 360 / bombNumber;
                bomb.transform.position = bombSpawn.transform.position;
                bomb.transform.localEulerAngles = direction;
                bomb.transform.DORotate(new Vector3(180, bomb.transform.localEulerAngles.y, bomb.transform.localEulerAngles.z), bombRotationTime).SetEase(Ease.InQuint);
                bomb.GetComponent<Rigidbody>().velocity = bomb.transform.up * bombspeed;
                bomb.GetComponent<BombScript>().parent = transform;
                
                //_stake.transform.parent = transform;
                bombList.Add(bomb);
            }
            else
            {
                GameObject bomb = Instantiate(bombaboom);
                //GameObject bomb = Instantiate(bombaboom);
                direction.y += 360 / bombNumber;
                bomb.transform.position = bombSpawn.transform.position;
                bomb.transform.localEulerAngles = direction;
                bomb.transform.DORotate(new Vector3(180, bomb.transform.localEulerAngles.y, bomb.transform.localEulerAngles.z), bombRotationTime).SetEase(Ease.InQuint);
                bomb.GetComponent<Rigidbody>().velocity = bomb.transform.up * bombspeed;
                bomb.GetComponent<BombScript>().parent = transform;
                
                //_stake.transform.parent = transform;
                bombList.Add(bomb);
            }

           
        }
        //stats.onAir = true;
    }
    private void HandleLluviaDeLasers()
    {
        t1 += Time.deltaTime;
        bool1 = true;
        //isAttacking = true;

        if (t1 > lluviaDelay && container1 < lluviaAmount)
        {
            container1++;
            t1 = 0;
            GameObject obj = Instantiate(lluviaDeLasers);
            obj.transform.position = player.transform.position + new Vector3(0, lluviaLasersHeight, 0);
            obj.GetComponent<LluviaLasers>().damage = 0;
        }
        else if (container1 >= lluviaAmount && bool1)
        {
            bool1 = false;
            t1 = 0;
            container1 = 0;
            //isAttacking = false;
            //currentAction = Maction.idle;
        }

    }

    public void PreparacionRayo()
    {
        disparado = false;
        if (rayoenrango == false && disparado == true)
        {
            t1 = 0;
            rayoenrango = true;
        }
    }

    private void RayoPalma()
    {
        t1 += Time.deltaTime;
        //stats.isAttacking = true;
        if (disparado == false)
        {
            vistaCazador.SetActive(true);
        }
        //vistaCazador.SetActive(true);
        //stats.canRotate = false;

        //MeganeuraLaser laser = vistaCazador.GetComponent<MeganeuraLaser>();
        laser.damage = currentDamageValue;
        laser.rotationSpeed = laserRotationSpeed;
        laser.damagePerTick = false;

        if (t1 > cazadorAimTime * 0.99f && t1 < cazadorAimTime)
        {
            laser.canRotate = false;
        }
        else if (t1 >= cazadorAimTime)
        {
            laser.isActive = true;
        }

        if (t1 >= cazadorAimTime + cazadorLaserDuration)
        {
            vistaCazador.SetActive(false);
            disparado = true;
            //currentAction = Maction.idle;
            //stats.isAttacking = false;
            t1 = 0;
            //stats.canRotate = true;
        }
    }

    public void BombasDivididas()
    {

        for (int i = 0; i < bombNumber; i++) //cambiar el 8 por variable
        {
            GameObject bomb = Instantiate(bombaboom);
            //direction.y += 360/bombNumber;
            bomb.transform.position = bombSpawn.transform.position;
            bomb.transform.localEulerAngles = direction2;
            bomb.transform.DORotate(new Vector3(180, bomb.transform.localEulerAngles.y, bomb.transform.localEulerAngles.z), bombRotationTime).SetEase(Ease.InQuint);
            bomb.GetComponent<Rigidbody>().velocity = bomb.transform.up * bombspeed;
            bomb.GetComponent<BombScript>().parent = transform;

            //_stake.transform.parent = transform;
            bombList.Add(bomb);
        }
        //stats.onAir = true;
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, closeDist);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, mediumDist);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, farDist);



    }

}



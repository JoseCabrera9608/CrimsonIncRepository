using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum Agtion
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
    public float t1;
    public float cazadorLaserDuration;
    public float cazadorAimTime;
    public float laserRotationSpeed;
    public float currentDamageValue;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
    }

    public void Iddle()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist <= closeDist)
        {
            if (bombList.Count == 0)
            {
                Bombas();
            }
        }
        if (dist <= mediumDist && dist > closeDist)
        {
            RayoPalma();
        }
        if (dist > mediumDist)
        {

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

    private void RayoPalma()
    {
        t1 += Time.deltaTime;
        //stats.isAttacking = true;
        vistaCazador.SetActive(true);
        //stats.canRotate = false;

        MeganeuraLaser laser = vistaCazador.GetComponent<MeganeuraLaser>();
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



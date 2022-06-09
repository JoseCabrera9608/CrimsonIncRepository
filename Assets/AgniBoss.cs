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

    public GameObject bombaboom;
    public GameObject bombaIce;
    public List<GameObject> bombList;

    public int bombNumber;
    public float bombspeed;
    public float bombRotationTime;

    public Vector3 direction2;
    public GameObject bombSpawn;

    public bool modoNitrogeno;

    // Start is called before the first frame update
    void Start()
    {
        //Bombas();
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
            Bombas();
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
}

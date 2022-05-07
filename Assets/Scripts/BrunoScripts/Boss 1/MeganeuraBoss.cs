using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeganeuraBoss : MonoBehaviour
{
    [SerializeField]public Action attacks;
    private MeganeuraStats stats;
    private Dictionary<Action,float> damages;

    public GameObject stake;
    public List<GameObject> stakeList;
    public Transform[] posTransform;
    private Vector3[] pos=new Vector3[2];
    Tween bobing;
    private void Start()
    {
        stats = GetComponent<MeganeuraStats>();
        damages = stats.attacksDamage;
        bobing.Pause();

        StartCoroutine(SpawnStakes());
        SetPos();
    }
    private void Update()
    {
        //Test
        if (Input.GetMouseButtonDown(0)) GravityStake();

        StakesCheck();
    }

    private void GravityStake()
    {
        Vector3 direction;
        
        for(int i = 0; i < stats.stakesToThrow; i++)
        {
            direction.y = Random.Range(0,361);
            direction.x = 100;
            direction.z = 0;

            GameObject _stake = Instantiate(stake);
            _stake.transform.position = transform.position;
            _stake.transform.localEulerAngles = direction;
            _stake.GetComponent<Rigidbody>().velocity = _stake.transform.up*50;
            _stake.transform.parent = transform;
            stakeList.Add(_stake);
        }
        stats.onAir = true;
    }
    private void StakesCheck()
    {
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
}


public enum Action
{
    estacas,
    rayoIon,
    bombardeoMisiles,
    rayosEmp,
    bombaFlash,
    bombaNapalm,
    vistaCazador,
    discosEmp
};

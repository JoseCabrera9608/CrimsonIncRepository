using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{

    public FireTrap fireTrap;
    public GameObject Player;
    public SkinnedMeshRenderer playermesh;
    public PlayerStatus playerStatus;
    public float dmgpersecond;
    public float initialdmg;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = Player.GetComponent<PlayerStatus>();
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //PlayerSingleton.Instance.playerCurrentHP = -11;

            if (fireTrap.traptype == "Fire")
            {
                PlayerStatus.damagePlayer?.Invoke(initialdmg);
            }

            if (fireTrap.traptype == "Ice")
            {
                PlayerStatus.damagePlayer?.Invoke(11);
                playerStatus.Ice();
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (fireTrap.traptype == "Fire")
        {
            PlayerStatus.damagePlayer?.Invoke(Time.deltaTime * dmgpersecond);
        }
    }
}

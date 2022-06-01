using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{

    public FireTrap fireTrap;
    public GameObject Player;
    public SkinnedMeshRenderer playermesh;
    public Material iceMat;
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
                PlayerSingleton.Instance.playerCurrentHP -= initialdmg;
            }

            if (fireTrap.traptype == "Ice")
            {
                PlayerSingleton.Instance.playerCurrentHP = -11;
                playermesh.material = iceMat;
                playerStatus.Ice();
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (fireTrap.traptype == "Fire")
        {
            PlayerSingleton.Instance.playerCurrentHP -= Time.deltaTime * dmgpersecond;
        }
    }
}

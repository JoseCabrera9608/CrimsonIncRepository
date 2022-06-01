using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{

    public FireTrap fireTrap;
    public GameObject Player;
    public SkinnedMeshRenderer playermesh;
    public Material iceMat;
    public Animator playeranim;
    public PlayerStatus playerStatus;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = Player.GetComponent<PlayerStatus>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP = -11;

            if (fireTrap.traptype == "Ice")
            {
                playermesh.material = iceMat;
                //playeranim.SetBool("Frozen", true);
                playerStatus.Freeze();
            }

        }
    }
}

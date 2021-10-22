using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator playeranim;
    public float timer;
    public float attacktimer;
    public bool attacking;
    public GameObject Pause;


    void Start()
    {
        attacktimer = 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {

        attacktimer += Time.deltaTime;

        PauseController pause = Pause.GetComponent<PauseController>();

        if (Input.GetKeyDown(KeyCode.Mouse0) && attacking == false && pause.pause == false)
        {

            attacking = true;
            playeranim.SetBool("Attack", true);
            

        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && attacktimer >= 0.7)
        {
            
            attacking = false;
            attacktimer = 0;
        }

        if (attacktimer >= 0.5)
        {
            playeranim.SetBool("Attack", false);
            
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            playeranim.SetBool("Attack", false);
            attacking = false;

        }
    }

    void Attacking()
    {
        attacking = true;
    }
    void AfterAttack()
    {
        attacking = false;
    }
}

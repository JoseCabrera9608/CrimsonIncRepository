using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator playeranim;
    public float timer;
    public float attacktimer;
    public bool attacking;
    public GameObject Pause;
    public BoxCollider weaponCollider;
    public float damage;
    public bool attackStatus;
    public float staminaAttack;
    public AudioSource audioSource;
    public bool combo;

    public Movement playermov;
    public PlayerStatus playerStatus;
    public VisualEffect vfx;


    void Start()
    {
        playermov = this.GetComponent<Movement>();
        playerStatus = this.GetComponent<PlayerStatus>();
        audioSource = this.GetComponent<AudioSource>();

        attacktimer = 0.5f;
        weaponCollider = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<BoxCollider>();

        weaponCollider.enabled = false;

        damage = 5;
        
    }

    // Update is called once per frame
    void Update()
    {

        attacktimer += Time.deltaTime;

        if (playerStatus.interacting == true)
        {
            return;
        }

        //PauseController pause = Pause.GetComponent<PauseController>();

        if (Input.GetKeyDown(KeyCode.Mouse0) && attacking == false  && PlayerSingleton.Instance.playerCurrentStamina >= 0.1f * playermov.staminaMax)
        {
            attacking = true;
            //attackStatus = true;
            //playermov.Recovery();
            playeranim.SetBool("Attack", true);
            playeranim.SetTrigger("AttackTrigger");
            //audioSource.Play();
            //playermov.stamina -= 0.25f * playermov.staminaMax;




        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && attacktimer >= 0.1f)
        {

          //FindObjectOfType<AudioManager>().Play("Ataque");
            attacking = false;

            attacktimer = 0;
        }

        if (attacktimer >= 0.1)
        {
            playeranim.SetBool("Attack", false);
            
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            playeranim.SetBool("Attack", false);
            attacking = false;

        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && combo == true)
        {
            playeranim.SetBool("Combo", true);
            
            playermov.Recovery();
            //playermov.Recovery();

        }




    }

    void ComboStateTrue()
    {
        combo = true;
    }

    void ComboStateFalse()
    {
        combo = false;
        playeranim.SetBool("Combo", false);
    }

    void IddleState()
    {

    }

    void Attacking()
    {
        attacking = true;
        weaponCollider.enabled = true;
        vfx.Play();
        audioSource.Play();
        playermov.Recovery();
        PlayerSingleton.Instance.playerCurrentStamina -= (0.01f * staminaAttack) * playermov.staminaMax;
    }
    void AfterAttack()
    {
        attacking = false;
        weaponCollider.enabled = false;
        //vfx.Stop();
    }
}

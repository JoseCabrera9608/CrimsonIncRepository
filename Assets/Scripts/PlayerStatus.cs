using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public float playerlife;
    public PlayerAttack playerAttack;
    public static Vector3 lastPosition;
    public bool incheck;
    public Material matNormal;
    public Material matHitted;
    public Animator anim;
    public SkinnedMeshRenderer mesh;
    public MeshRenderer armaMesh;
    public Material armaMatNormal;
    public Material armaMatHitted;

    public ProgressManager progress;
    public Transform InteractualObject;

    public static Action onPlayerDeath;


    public float timer;
    public float healingtime;
    public float healingAmount;
    public float maxtimehealing;
    
    public bool hiteado;
    public bool pruebasingle;
    public bool interacting;
    public bool activeinteraction;
    public bool playerdeath;

    public int lvl;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerSingleton.Instance.playerCurrentHP -= 50;
        //playermov.enabled = false;

        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        playerAttack = this.GetComponent<PlayerAttack>();
        timer = maxtimehealing;

        if (lvl == 1)
        {
            progress.lastposition = new Vector3(57, -3, 7);
            //progress.lastposition = new Vector3(57, 0, -200);
        }

        //playerlife = 100;

        //playermov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSingleton.Instance.playerCurrentHP <= 0 && lvl ==0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(gameObject);
        }

        if (PlayerSingleton.Instance.playerCurrentHP <= 0 && lvl == 1)
        {
            playerdeath = true;
            Death();
        }
        else
        {
            playerdeath = false;
        }

        if (hiteado == true)
        {
            StartCoroutine(HittedCoroutine());
        }
        SingletonConnect();

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            lvl = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            lvl = 1;
        }

        playerlife = PlayerSingleton.Instance.playerCurrentHP;
        pruebasingle = PlayerSingleton.Instance.playerHitted;

        AnimationStatus();
        Healing();

        //StartCoroutine(HittedCoroutine());
        if (PlayerSingleton.Instance.playerHitted == true)
        {
            StartCoroutine(HittedCoroutine());
        }
    }

    private void FixedUpdate()
    {
        if (interacting == true)
        {
            transform.LookAt(InteractualObject);
        }
    }

    public void Death()
    {
        anim.SetBool("Death", true);
        this.transform.position = progress.lastposition;
        onPlayerDeath?.Invoke();
        PlayerSingleton.Instance.playerCurrentHP = PlayerSingleton.Instance.playerMaxHP;
        PlayerSingleton.Instance.playerCurrentHealingCharges = PlayerSingleton.Instance.playerMaxHealingCharges;
    }

    public void AfterDeath()
    {
        anim.SetBool("Death", false);
        this.transform.position = progress.lastposition;
        PlayerSingleton.Instance.playerCurrentHP = PlayerSingleton.Instance.playerMaxHP;
        PlayerSingleton.Instance.playerCurrentHealingCharges = PlayerSingleton.Instance.playerMaxHealingCharges;
    }

    void SingletonConnect()
    {
        healingAmount = PlayerSingleton.Instance.playerHealAmount;

    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("DmgArea") && playermov.fallVelocity <= 0)
        {
            playerlife -= 40;
        }*/


    }
    private void OnParticleTrigger()
    {
        Debug.Log("ParticleColl");
        playerlife -= 10;
    }

    public void Healing()
    {
        timer += Time.deltaTime;

        if (timer <= healingtime && PlayerSingleton.Instance.playerCurrentHealingCharges > 0)
        {
            //PlayerSingleton.Instance.playerCurrentHP += (healingAmount/healingtime)*Time.deltaTime;
            if (PlayerSingleton.Instance.playerCurrentHP < PlayerSingleton.Instance.playerMaxHP)
            {
                PlayerSingleton.Instance.playerCurrentHP += (healingAmount / healingtime) * Time.deltaTime; ;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && timer > healingtime && PlayerSingleton.Instance.playerCurrentHP < PlayerSingleton.Instance.playerMaxHP)
        {
            timer = 0;
            healingtime = (0.01f * maxtimehealing)* healingAmount;
            //PlayerSingleton.Instance.playerCurrentHP += 10;
            PlayerSingleton.Instance.playerCurrentHealingCharges -= 1;
        }
    }

    void AnimationStatus()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            playerAttack.attackStatus = true;
        }
        else
        {
            playerAttack.attackStatus = false;
        }

    }

    public void Interacting()
    {
       
        interacting = true;
        //anim.SetBool("Interact", true);
        anim.SetTrigger("Interact");
        
        //transform.LookAt(InteractualObject);
    }

    public void ActiveInteraction()
    {

        activeinteraction = true;
    }

    public void AfterInteracting()
    {
        interacting = false;
        activeinteraction = false;
        //anim.SetBool("Interact", false);
        //transform.LookAt(InteractualObject);
    }

    IEnumerator HittedCoroutine()
    {

        mesh.material = matHitted;
        armaMesh.material = armaMatHitted;
        healingtime = 0;

        yield return new WaitForSeconds(0.5f);

        PlayerSingleton.Instance.playerHitted = false;
        hiteado = false;
        mesh.material = matNormal;
        armaMesh.material = armaMatNormal;
    }
}


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

    public Transform InteractualObject;

    public float timer;
    public float healingtime;
    public float healingAmount;
    public float maxtimehealing;
    
    public bool hiteado;
    public bool pruebasingle;
    public bool interacting;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerSingleton.Instance.playerCurrentHP -= 50;
        //playermov.enabled = false;

        playerAttack = this.GetComponent<PlayerAttack>();
        timer = maxtimehealing;

        //playerlife = 100;

        //playermov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSingleton.Instance.playerCurrentHP <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(gameObject);
        }

        if (hiteado == true)
        {
            StartCoroutine(HittedCoroutine());
        }


        if (PlayerSingleton.Instance.playerCurrentHP > 100)
        {
            PlayerSingleton.Instance.playerCurrentHP = 100;
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
            PlayerSingleton.Instance.playerCurrentHP += (healingAmount/healingtime)*Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && timer > healingtime)
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

    public void AfterInteracting()
    {
        interacting = false;
        //anim.SetBool("Interact", false);
        //transform.LookAt(InteractualObject);
    }

    IEnumerator HittedCoroutine()
    {

        mesh.material = matHitted;
        healingtime = 0;

        yield return new WaitForSeconds(0.5f);

        PlayerSingleton.Instance.playerHitted = false;
        hiteado = false;
        mesh.material = matNormal;
    }
}


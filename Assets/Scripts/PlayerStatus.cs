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

    public float timer;
    public float healingtime;
    public float healingAmount;
    
    public bool hiteado;
    public bool pruebasingle;


    // Start is called before the first frame update
    void Start()
    {
        PlayerSingleton.Instance.playerCurrentHP -= 50;
        //playermov.enabled = false;

        playerAttack = this.GetComponent<PlayerAttack>();

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
        //Healing();
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

        if (timer <= healingtime)
        {
            PlayerSingleton.Instance.playerCurrentHP += (healingAmount/healingtime)*Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q) && timer > healingtime)
        {
            timer = 0;
            healingtime = (0.01f * 6)* healingAmount;
            //PlayerSingleton.Instance.playerCurrentHP += 10;
            PlayerSingleton.Instance.playerCurrentHealingCharges -= 1;
        }
    }

    void AnimationStatus()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            playerAttack.attackStatus = true;
        }
        else
        {
            playerAttack.attackStatus = false;
        }
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


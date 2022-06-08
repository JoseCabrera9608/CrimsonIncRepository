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
    public Material iceMat;
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
    public bool dying;

    public int lvl;

    public Rigidbody rb;

    public GameObject freezeParticles;

    public RigidbodyConstraints rigidbodyConstraints;

    public static Action<float> damagePlayer;

    public GameObject healingEffect;
    // Start is called before the first frame update
    private void OnEnable()
    {
        damagePlayer += OnTakeDamage;
    }
    private void OnDisable()
    {
        damagePlayer -= OnTakeDamage;
    }
    void Start()
    {
        //PlayerSingleton.Instance.playerCurrentHP -= 50;
        //playermov.enabled = false;
        
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        playerAttack = this.GetComponent<PlayerAttack>();
        rb = this.GetComponent<Rigidbody>();
        rigidbodyConstraints = rb.constraints;

        timer = maxtimehealing;

        transform.position = progress.lastposition;

        if (lvl == 0)
        {
            //progress.lastposition = new Vector3(80, 0, -110);
            //progress.lastposition = new Vector3(57, 0, -200);
        }

        if (lvl == 1)
        {
            //progress.lastposition = new Vector3(57, -3, 4);
            //progress.lastposition = new Vector3(57, 0, -200);
        }
        if (lvl == 2)
        {
            progress.lastposition = new Vector3(57, 6, 255);
            //progress.lastposition = new Vector3(57, -3, 125);
            //progress.lastposition = new Vector3(57, 0, -200);
        }

        if (lvl == 3)
        {
            progress.lastposition = new Vector3(100, -3, 65);
            //transform.position = progress.lastposition;
            //progress.lastposition = new Vector3(57, -3, 125);
            //progress.lastposition = new Vector3(57, 0, -200);
        }

        //playerlife = 100;

        //playermov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSingleton.Instance.playerCurrentHP <= 0 && playerdeath == false)
        {
            PreDeath();
        }

        if (hiteado == true)
        {
            StartCoroutine(HittedCoroutine());
        }
        SingletonConnect();

        if (progress.resetlvl == true)
        {
            lvl = 0;
        }
        else
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

    public void OnTakeDamage(float damage)
    {
        Movement playermov = GetComponent<Movement>();

        if (playermov.isDashing==false)
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
        }
        
        
    }

    public void ReloadDeath()
    {
        //Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(gameObject);
    }

    public void PreDeath()
    {
        anim.SetTrigger("Death");
        anim.SetBool("DeathBool", true);
        anim.SetBool("Falling", false);
        playerdeath = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<Movement>().enabled = false;

    }

    public void Death()
    {
        onPlayerDeath?.Invoke();

        if (lvl == 0 || lvl == 3)
        {
            //Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Destroy(gameObject);
        }

        if (lvl == 1 || lvl == 2)
        {
            anim.SetTrigger("DeathUp");
            anim.SetBool("DeathBool", false);
            this.transform.position = progress.lastposition;
            PlayerSingleton.Instance.playerCurrentHP = PlayerSingleton.Instance.playerMaxHP;
            PlayerSingleton.Instance.playerCurrentHealingCharges = PlayerSingleton.Instance.playerMaxHealingCharges;
            
        }

    }

    public void AfterDeath()
    {
        GetComponent<Movement>().enabled = true;
        rb.constraints = rigidbodyConstraints;
        playerdeath = false;
        
    }

    void SingletonConnect()
    {
        healingAmount = PlayerSingleton.Instance.playerHealAmount;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Acid"))
        {
            anim.SetTrigger("Acid");
            dying = true;
        }
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
            StartCoroutine(HealingEffectDuration());
            timer = 0;
            healingtime = (0.01f * maxtimehealing)* healingAmount;
            //PlayerSingleton.Instance.playerCurrentHP += 10;
            PlayerSingleton.Instance.playerCurrentHealingCharges -= 1;
        }
    }
    IEnumerator HealingEffectDuration()
    {
        healingEffect.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        healingEffect.SetActive(false);
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

    public void Freeze()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        
        anim.enabled = false;
        GetComponent<Movement>().enabled = false;
    }

    public void UnFreeze()
    {
        anim.enabled = true;
        PlayerSingleton.Instance.playerFreezed = false;
        if (playerdeath == false)
        {
            rb.constraints = rigidbodyConstraints;
            GetComponent<Movement>().enabled = true;
            mesh.material = matNormal;
            
            anim.Play("Iddle");
        }
    }

    public void Ice()
    {
        PlayerSingleton.Instance.playerFreezed = true;
        mesh.material = iceMat;
        freezeParticles.SetActive(true);
        freezeParticles.GetComponent<ParticleSystem>().Clear();
        freezeParticles.GetComponent<ParticleSystem>().Play();
        //anim.enabled = false;
        //anim.Play("FrozDeath");
        Freeze();
    }

    IEnumerator HittedCoroutine()
    {

        //mesh.material = matHitted;
        mesh.material.SetColor("_EmissionColor", Color.red);
        armaMesh.material.SetColor("_EmissionColor", Color.red);
        healingtime = 0;

        yield return new WaitForSeconds(0.5f);

        PlayerSingleton.Instance.playerHitted = false;
        hiteado = false;
        mesh.material.SetColor("_EmissionColor", Color.cyan);
        armaMesh.material.SetColor("_EmissionColor", Color.cyan);
    }
}


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
    public bool hiteado;
    public bool pruebasingle;


    // Start is called before the first frame update
    void Start()
    {
        //PlayerSingleton.Instance.playerCurrentHP -= 50;
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

        pruebasingle = PlayerSingleton.Instance.playerHitted;

        AnimationStatus();
        //StartCoroutine(HittedCoroutine());
        if (PlayerSingleton.Instance.playerHitted == true)
        {
            StartCoroutine(HittedCoroutine());
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

        yield return new WaitForSeconds(0.5f);

        PlayerSingleton.Instance.playerHitted = false;
        hiteado = false;
        mesh.material = matNormal;
    }
}


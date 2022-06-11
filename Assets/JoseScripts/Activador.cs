using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activador : MonoBehaviour
{
    // Start is called before the first frame update
    bool colliding;
    public PlayerStatus playerStatus;
    public GameObject Player;
    public Transform consolepoint;
    public GameObject Puerta;
    public MeshRenderer mesh;
    //public Material matActivated;
    Animator puertaAnim;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = Player.GetComponent<PlayerStatus>();
        puertaAnim = Puerta.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && colliding == true)
        {
            //progress.checkpointIndex = checkpointIndex;
            //progress.lastposition = SpawnPoint.transform.position;
            FindObjectOfType<AudioManager>().Play("Checkpoint");


            playerStatus.InteractualObject = consolepoint;
            playerStatus.Interacting();
            StartCoroutine(ChangeColor());
            puertaAnim.SetTrigger("Activado");
            
        }

    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(1.5f);
        mesh.material.SetColor("_EmissionColor", Color.cyan);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            colliding = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            colliding = false;
        }
    }
}

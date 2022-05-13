using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{

    public int checkpointIndex;
    public bool colliding;

    public MeshRenderer mesh;
    public Material normalMat;
    public Material activeMat;

    public ProgressManager progress;
    public GameObject Player;
    public GameObject SpawnPoint;
    public PlayerStatus playerStatus;

    public Transform consolepoint;

    // Start is called before the first frame update
    void Start()
    {
        
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        mesh = this.GetComponent<MeshRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = Player.GetComponent<PlayerStatus>();



        if (progress.lastposition != Vector3.zero)
        {
            Player.transform.position = progress.lastposition;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (checkpointIndex == progress.checkpointIndex)
        {
            mesh.material = activeMat;

        }
        else
        {
            mesh.material = normalMat;
        }

        if (Input.GetKeyDown(KeyCode.E) && colliding ==true)
        {
            //progress.checkpointIndex = checkpointIndex;
            //progress.lastposition = SpawnPoint.transform.position;
            FindObjectOfType<AudioManager>().Play("Checkpoint");


            playerStatus.InteractualObject = consolepoint;
            playerStatus.Interacting();
        }

        if (playerStatus.activeinteraction == true && colliding == true)
        {
            progress.checkpointIndex = checkpointIndex;
            progress.lastposition = SpawnPoint.transform.position;
            PlayerSingleton.Instance.playerCurrentHP = PlayerSingleton.Instance.playerMaxHP;
            PlayerSingleton.Instance.playerCurrentHealingCharges = PlayerSingleton.Instance.playerMaxHealingCharges;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //progress.lastposition = new Vector3(0, 0, 0);
            SceneManager.LoadScene("WB_Diego");

            //ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            progress.lastposition = new Vector3(57, -3, 6);
            SceneManager.LoadScene("TinocoMirror");
            //SceneManager.LoadScene("EnriqueTest2");

            //ChangeScene();
        }
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

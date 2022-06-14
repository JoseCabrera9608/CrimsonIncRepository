using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{

    public int checkpointIndex;
    public bool colliding;

    public MeshRenderer mesh;
    public MeshRenderer lowmesh;
    public Material normalMat;
    public Material activeMat;

    public ProgressManager progress;
    public GameObject Player;
    public GameObject SpawnPoint;
    public PlayerStatus playerStatus;

    public Transform consolepoint;

    public float timer;
    public float timer2;
    public bool limit1;
    public bool limit2;
    public bool resetlvl;

    // Start is called before the first frame update
    void Start()
    {
        
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        //mesh = this.GetComponent<MeshRenderer>();
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
            //mesh.material = activeMat;

            timer += Time.deltaTime;
            //timer2 += Time.deltaTime;

            mesh.material.SetColor("_EmissionColor", Color.cyan *5);
            lowmesh.material.SetColor("_EmissionColor", Color.cyan *5);

            /*if (timer < 2)
            {
                timer2 += 2 * Time.deltaTime;
                mesh.material.SetColor("_EmissionColor", Color.cyan * timer2 * 3);
                lowmesh.material.SetColor("_EmissionColor", Color.cyan * timer2 * 3);
            }
            if (timer >= 2 && timer <= 4)
            {
                timer2 -= 2 * Time.deltaTime;
                mesh.material.SetColor("_EmissionColor", Color.cyan * timer2 * 3);
                lowmesh.material.SetColor("_EmissionColor", Color.cyan * timer2 * 3);

            }
            if (timer > 4)
            {
                timer = 0;
            }
            */
        }
        else
        {
            timer += Time.deltaTime;
            //timer2 += Time.deltaTime;

            if (timer < 2)
            {
                timer2 += 2*Time.deltaTime;
                mesh.material.SetColor("_EmissionColor", Color.red * timer2 * 3);
                lowmesh.material.SetColor("_EmissionColor", Color.red * timer2 * 3);
            }
            if (timer >=2 && timer <=4)
            {
                timer2 -= 2*Time.deltaTime;
                mesh.material.SetColor("_EmissionColor", Color.red * timer2 * 3);
                lowmesh.material.SetColor("_EmissionColor", Color.red * timer2 * 3);

            }
            if (timer > 4)
            {
                timer = 0;
            }

            //mesh.material = normalMat;
            //mesh.material.SetColor("_EmissionColor", Color.red *4);
            //lowmesh.material.SetColor("_EmissionColor", Color.red *4);
        }

        if (Input.GetKeyDown(KeyCode.E) && colliding ==true)
        {
            //progress.checkpointIndex = checkpointIndex;
            //progress.lastposition = SpawnPoint.transform.position;
            FindObjectOfType<AudioManager>().Play("Checkpoint");


            playerStatus.InteractualObject = consolepoint;
            playerStatus.Interacting();
            if (resetlvl == true)
            {
                progress.resetlvl = true;
            }
            else
            {
                progress.resetlvl = false;
            }
        }

        if (playerStatus.activeinteraction == true && colliding == true)
        {
            progress.checkpointIndex = checkpointIndex;
            progress.lastposition = SpawnPoint.transform.position;
            PlayerSingleton.Instance.playerCurrentHP = PlayerSingleton.Instance.playerMaxHP;
            PlayerSingleton.Instance.playerCurrentHealingCharges = PlayerSingleton.Instance.playerMaxHealingCharges;
        }

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            progress.lastposition = new Vector3(80, 0, -110);
            SceneManager.LoadScene("Lvl0");

            //ChangeScene();
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            progress.lastposition = new Vector3(57, -3, 6);
            SceneManager.LoadScene("Lvl1");
            //SceneManager.LoadScene("EnriqueTest2");

            //ChangeScene();
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            progress.lastposition = new Vector3(58, 0, 115);
            SceneManager.LoadScene("Lvl2");

            //ChangeScene();
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            progress.lastposition = new Vector3(100, 63, 133);
            SceneManager.LoadScene("Lvl0");

            //ChangeScene();
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            progress.lastposition = new Vector3(57, 0, -196);
            SceneManager.LoadScene("Lvl1");

            //ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerSingleton.Instance.playerCurrentHP = -11;
        }
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            progress.lastposition = new Vector3(57, 6, 255);
            SceneManager.LoadScene("Lvl2");

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

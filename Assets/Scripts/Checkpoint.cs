using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{

    public int checkpointIndex;

    public MeshRenderer mesh;
    public Material normalMat;
    public Material activeMat;

    public ProgressManager progress;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        mesh = this.GetComponent<MeshRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");

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

        if (Input.GetKeyDown(KeyCode.E))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            progress.lastposition = new Vector3(0, 0, 0);
            SceneManager.LoadScene("EnriqueTest");

            //ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            progress.lastposition = new Vector3 (7,0,-6);
            SceneManager.LoadScene("EnriqueTest2");

            //ChangeScene();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                progress.checkpointIndex = checkpointIndex;
                progress.lastposition = transform.position;
            }
        }
    }
}

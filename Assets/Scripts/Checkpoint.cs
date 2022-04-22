using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

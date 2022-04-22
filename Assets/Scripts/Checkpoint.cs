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

    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        mesh = this.GetComponent<MeshRenderer>();

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
    }
}

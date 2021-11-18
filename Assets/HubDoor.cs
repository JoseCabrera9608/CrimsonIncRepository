using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubDoor : MonoBehaviour
{
    // Start is called before the first frame update

    public int lvl;
    public ProgressManager progress;
    public MeshRenderer mesh;
    public Material greenmaterial;
    public Material normalmaterial;

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        mesh = GetComponent<MeshRenderer>();
        mesh.material = normalmaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (lvl == 1 && progress.tutorial == true)
        {
            mesh.material = greenmaterial;
        }

        if (lvl == 3 && progress.level2 == true)
        {
            mesh.material = greenmaterial;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        SceneController scene = GetComponent<SceneController>();

        if (other.gameObject.CompareTag("Player"))
        {
            if (lvl == 1 && progress.tutorial == false)
            {
                scene.ChangeScene();
            }
            if (lvl == 2 && progress.level1 == false)
            {
                scene.ChangeScene();
            }
            if (lvl == 3 && progress.level2 == false)
            {
                scene.ChangeScene();
            }
            if (lvl == 4 && progress.level3 == false)
            {
                scene.ChangeScene();
            }

        }
    }
}

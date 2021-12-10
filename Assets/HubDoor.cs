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
    public SceneController scene;
    public Fade fade;
    public bool lvlgo;

    public Vector3 initialpos;


    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
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

        if (lvl == 2 && progress.level1 == true)
        {
            mesh.material = greenmaterial;
        }

        if (lvl == 3 && progress.level2 == true)
        {
            mesh.material = greenmaterial;
        }
        if (lvl == 4 && progress.level3 == true)
        {
            mesh.material = greenmaterial;
        }

        if (fade.fadeinend == true && lvlgo == true)
        {
            progress.lastposition = initialpos;
            scene.ChangeScene();
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            

            
            if (lvl == 1 && progress.tutorial == false)
            {

                fade.Fadein();
                lvlgo = true;



            }
            if (lvl == 2 && progress.level1 == false)
            {
                fade.Fadein();
                lvlgo = true;


            }
            if (lvl == 3 && progress.level2 == false)
            {
                fade.Fadein();
                lvlgo = true;


            }
            if (lvl == 4 && progress.level3 == false)
            {
                fade.Fadein();
                lvlgo = true;

            }

        }
    }
}

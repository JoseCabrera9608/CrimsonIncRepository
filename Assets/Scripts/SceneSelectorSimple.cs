using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectorSimple : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    public Vector3 hubpos;
    public ProgressManager progress;

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            sceneName = "VientreRudra";
            //progress.lastposition = progress.hubpos;


            //ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            sceneName = "Almacenamiento";
            //progress.lastposition = progress.hubpos;


            //ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            sceneName = "Fabrica";
            //progress.lastposition = progress.hubpos;


            //ChangeScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            sceneName = "Dulscene";
            //progress.lastposition = progress.hubpos;


            //ChangeScene();
        }





    }

    public void ChangeScene()
    {

        SceneManager.LoadScene(sceneName);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            progress.lastposition = hubpos;
            SceneManager.LoadScene(sceneName);
        }
    }




}

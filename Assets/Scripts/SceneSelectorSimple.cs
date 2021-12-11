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

        if (Input.GetKeyDown(KeyCode.M))
        {

            sceneName = "Menu";

            ChangeScene();
        }


        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

            sceneName = "Hub";
            progress.lastposition = progress.hubpos;


            ChangeScene();
        }

    }

    public void ChangeScene()
    {

        SceneManager.LoadScene(sceneName);

    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelectorSimple : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {

            sceneName = "Menu";

            ChangeScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            sceneName = "Tutorial";
            
            ChangeScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            sceneName = "Boss1";

            ChangeScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            sceneName = "Boss2";

            ChangeScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            sceneName = "Boss3";

            ChangeScene();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

            sceneName = "SantiWhiteBlocking";

            ChangeScene();
        }

    }

    public void ChangeScene()
    {

        SceneManager.LoadScene(sceneName);

    }




}

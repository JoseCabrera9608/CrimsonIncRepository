using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update

    public string sceneName;
    public ProgressManager progress;

    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene()
    {

        SceneManager.LoadScene(sceneName);

    }

    public void ChangeTutorial()
    {
        progress.lastposition = new Vector3(430, 1, 0);
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitGame()
    {
        Debug.Log("Saliste del Juego");
        Application.Quit();
    }



}

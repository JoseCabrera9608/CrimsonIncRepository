using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerSceneChange : MonoBehaviour
{
    public float timer;
    public float timeToChange;
    public string sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToChange)
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}

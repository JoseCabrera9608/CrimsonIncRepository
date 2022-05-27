using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public string sceneToDestroy;
    public string actualScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        actualScene =  SceneManager.GetActiveScene().name;

        if (actualScene == sceneToDestroy)
        {
            Destroy(gameObject);

        }
    }

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Progress");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}

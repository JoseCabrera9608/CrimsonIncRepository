using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyOnlyOnScene : MonoBehaviour
{
    // Start is called before the first frame update

    public string sceneToDestroy;
    public string sceneToDestroy2;
    public string actualScene;
    public string objecttag;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(objecttag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        actualScene = SceneManager.GetActiveScene().name;

        if (actualScene == sceneToDestroy || actualScene == sceneToDestroy2)
        {
            Destroy(gameObject);

        }
    }
}

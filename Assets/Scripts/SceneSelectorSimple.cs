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

    }

    public void ChangeScene()
    {

        progress.enemysdeath = 0;
        SceneManager.LoadScene(sceneName);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            progress.enemysdeath = 0;
            progress.lastposition = hubpos;
            progress.enemysdeath = 0;
            SceneManager.LoadScene(sceneName);
        }
    }




}

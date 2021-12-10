using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathArea : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public bool contact;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.3f && contact == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer = 0;
            contact = true;
            Destroy(other.gameObject);


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject MenuPausa;
    public GameObject MenuSettings;
    public bool pause;
    
    
    void Start()
    {
        Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Activate();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Activate();
        }
    }

    public void Activate()
    {
        pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MenuPausa.SetActive(true);
    }

    public void Deactivate()
    {

        pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        MenuPausa.SetActive(false);
    }

    public void YesSettings()
    {
        MenuPausa.SetActive(false);
        MenuSettings.SetActive(true);
    }

    public void NoSettings()
    {
        MenuPausa.SetActive(true);
        MenuSettings.SetActive(false);
    }
}

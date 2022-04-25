using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleccionBoss : MonoBehaviour
{
    public GameObject komodoUI;
    public GameObject komodoObject;
    public GameObject raptorUI;
    public GameObject raptorObject;
    public GameObject calamarUI;
    public GameObject calamarObject;
    public GameObject spawnPoint;
    public GameObject seleccionUI;
    public GameObject gameManager;
    public GameObject EnemyHealthBar;
    Inicio inicioBool;

    private void Start()
    {
        inicioBool = gameManager.GetComponent<Inicio>();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)&&inicioBool.seInicio == true)
        {
            EleccionKomodo();
            seleccionUI.SetActive(false);
            EnemyHealthBar.SetActive(true);

        }
  

        if (Input.GetKeyDown(KeyCode.Alpha2) && inicioBool.seInicio == true)
        {
            EleccionRaptor();
            seleccionUI.SetActive(false);
        }
 

        if (Input.GetKeyDown(KeyCode.Alpha3) && inicioBool.seInicio == true)
        {
            EleccionCalamar();
            seleccionUI.SetActive(false);
        }
       
    }

    public void EleccionKomodo()
    {
        komodoObject.SetActive(true);
        komodoUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void EleccionRaptor()
    {
        raptorObject.SetActive(true);
        raptorUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void EleccionCalamar()
    {
        calamarObject.SetActive(true);
        calamarUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

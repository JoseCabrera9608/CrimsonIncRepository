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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EleccionKomodo();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EleccionRaptor();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EleccionCalamar();
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

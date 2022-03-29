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
 
    public void EleccionKomodo()
    {
        komodoObject.SetActive(true);
        komodoUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void EleccionRaptor()
    {
        GameObject temporalBoss = Instantiate(raptorObject);
        temporalBoss.transform.position = spawnPoint.transform.position;
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

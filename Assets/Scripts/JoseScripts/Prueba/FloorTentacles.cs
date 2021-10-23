using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTentacles : MonoBehaviour
{
    public GameObject[] trampas;
    GameObject chosenTrap;
    public GameObject tentaculo;
    public GameObject advertencia;
    int index;
    public float timer;
    public bool allowAdvertencia;

    // Use this for initialization
    void Start()
    {

        trampas = GameObject.FindGameObjectsWithTag("Trampas");
        allowAdvertencia = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {

            if (allowAdvertencia)
            {
                allowAdvertencia = false;
                CreateAdvertencia();
            }

            if (timer > 5)
            {
                allowAdvertencia = true;
                timer = 0;
                Create();
            }

        }

    }

    void Create()
    {
        GameObject temporalPuas = Instantiate(tentaculo);
        temporalPuas.transform.position = chosenTrap.transform.position;
        Destroy(temporalPuas, 5f);
    }
    void CreateAdvertencia()
    {
        index = Random.Range(0, trampas.Length);
        chosenTrap = trampas[index];
        GameObject temporalAdvertencia = Instantiate(advertencia);
        temporalAdvertencia.transform.position = chosenTrap.transform.position;
        Destroy(temporalAdvertencia, 2f);

    }
}

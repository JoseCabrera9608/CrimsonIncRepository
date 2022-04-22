using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    // Start is called before the first frame update

    public bool tutorial;
    public bool level1 = false;
    public bool level2 = false;
    public bool level3 = false;

    public int checkpointIndex;

    public float sens;
    public int lvl;
    public bool changing;

    public Sensibilidad sensi;

    private static ProgressManager instance;
    public Vector3 lastposition;
    public Vector3 hubpos;

    private void Awake()
    {
        sens = 0.5f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sensi = GameObject.FindGameObjectWithTag("SensController").GetComponent<Sensibilidad>();
        sens = sensi.slider.value;


    }
}

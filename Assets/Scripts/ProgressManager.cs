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
    public float vol;
    public int lvl;
    public bool changing;

    public bool resetlvl;

    public Sensibilidad sensi;

    private static ProgressManager instance;
    public Vector3 lastposition;
    public Vector3 hubpos;

    public int enemysdeath;

    private void Awake()
    {
        sens = 0.5f;
        vol = 0.5f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = vol;
        //sensi = GameObject.FindGameObjectWithTag("SensController").GetComponent<Sensibilidad>();
        //sens = sensi.slider.value;


    }
}

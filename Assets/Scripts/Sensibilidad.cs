using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Sensibilidad : MonoBehaviour
{
    public Slider slider;
    public float sensibilidad;
    public CinemachineFreeLook cinem;

    public ProgressManager progress;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {

        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();

        slider.value = progress.sens;
    }

    // Update is called once per frame
    void Update()
    {
        //sensibilidad = slider.value;
        cinem.m_YAxis.m_MaxSpeed = slider.value * 7;
        cinem.m_XAxis.m_MaxSpeed = slider.value * 700;
    }


}

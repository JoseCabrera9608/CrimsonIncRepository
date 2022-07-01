using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Sensibilidad : MonoBehaviour
{
    public Slider slider;
    public Slider volSlider; 
    public float sensibilidad;
    public CinemachineFreeLook cinem;
    public Text sensitext;
    public ProgressManager progress;

    // Start is called before the first frame update

    private void Awake()
    {

    }

    void Start()
    {

        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();

        slider.value = progress.sens;
        volSlider.value = progress.vol;
    }

    // Update is called once per frame
    void Update()
    {
        //sensibilidad = slider.value;
        if (sensitext != null)    sensitext.text = slider.value.ToString("F2");
        if (slider.value <= 0)
        {
            if (cinem != null)
            {
                cinem.m_YAxis.m_MaxSpeed = 0.7f;
                cinem.m_XAxis.m_MaxSpeed = 70;
            }
        }
        else
        {
            if (cinem != null)
            {
                cinem.m_YAxis.m_MaxSpeed = slider.value * 7;
                cinem.m_XAxis.m_MaxSpeed = slider.value * 700;
            }

        }
        progress.vol = volSlider.value;
        progress.sens = slider.value;
    }


}

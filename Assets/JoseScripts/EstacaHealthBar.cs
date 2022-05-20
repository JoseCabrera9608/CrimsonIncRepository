using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstacaHealthBar : MonoBehaviour
{
    public Image FillImage;
    public float health;
    public IDConexion vidaEstaca;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FillImage.fillAmount = vidaEstaca.vida / 10;
    }
}

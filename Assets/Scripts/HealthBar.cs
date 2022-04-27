using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image FillImage;
    //public int damage;
    public float health;
    //public float startHealth;
    public EnemiPequeñoControlador enemy;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //health = enemy.healthEnemigo;
        FillImage.fillAmount = enemy.healthEnemigo / 100;
    }

    public void OnTakeDamage()
    {
        //health = health - damage;
        //FillImage.fillAmount = health / enemy.healthEnemigo;
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CangrejoUI : MonoBehaviour
{
    public Image healthBar;
    BossCangrejo enemy;
    public float enemyhealth;
    public float maxhealth;
    
    public GameObject Enemy;
    public GameObject EnemyHealthBar;

    void Start()
    {
        //playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        enemy = Enemy.gameObject.GetComponent<BossCangrejo>();
        maxhealth = enemy.vidaActual;

        //enemy = Enemy.gameObject.GetComponent<BossCangrejo>();
        //enemy.vidaActual = 450;
    }

    // Update is called once per frame
    void Update()
    {
        enemyhealth = enemy.vidaActual;
        healthBar.fillAmount = enemyhealth / maxhealth;

        //enemyhealth = enemy.vidaActual;
        //healthBar.fillAmount = enemyhealth / 450;


        /* if (enemy.hitted == true)
           {
               enemyhealth -= 15;
               enemy.hitted = false;
           }
        */
        if (enemyhealth <= 0)
        {

            
            EnemyHealthBar.SetActive(false);

        }

    }
}

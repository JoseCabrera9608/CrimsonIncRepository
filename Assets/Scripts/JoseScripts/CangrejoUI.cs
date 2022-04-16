using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CangrejoUI : MonoBehaviour
{
    public Image healthBar;
    BossCangrejo enemy;
    public int enemyhealth;
    public GameObject Enemy;
    public GameObject EnemyHealthBar;

    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<BossCangrejo>();
        enemy.vidaActual = 150;
    }

    // Update is called once per frame
    void Update()
    {
        enemyhealth = enemy.vidaActual;
        healthBar.fillAmount = enemyhealth / 150f;


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

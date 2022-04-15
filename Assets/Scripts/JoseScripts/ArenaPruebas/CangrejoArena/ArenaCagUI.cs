using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArenaCagUI : MonoBehaviour
{
    public Image healthBar;
    CangrejoArena enemy;
    public int enemyhealth;
    public GameObject Enemy;
    public GameObject EnemyHealthBar;

    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<CangrejoArena>();
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
            
            Destroy(Enemy);
            EnemyHealthBar.SetActive(false);

        }
      
    }
}

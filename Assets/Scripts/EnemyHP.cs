using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBar;
    public EnemyDino enemy;
    public int enemyhealth;
    public GameObject Enemy;

    void Start()
    {
         enemy = GameObject.FindGameObjectWithTag("EnemyDino").GetComponent<EnemyDino>();
         enemy.health = enemyhealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = enemyhealth / 100f;
        enemy.health = enemyhealth;

        if (enemy.hitted == true)
        {
            enemyhealth -= 10;
            enemy.hitted = false;
        }

        if (enemyhealth <= 0)
        {
            Destroy(Enemy);
        }

    }
}

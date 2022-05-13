using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeganeuraUI : MonoBehaviour
{
    public Image healthBar;
    MeganeuraStats enemy;
    public float enemyhealth;
    public float maxhealth;
    public GameObject Enemy;
    public GameObject EnemyHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<MeganeuraStats>();
        maxhealth = enemy.health;
        //enemy.health = 150;
    }

    // Update is called once per frame
    void Update()
    {
        enemyhealth = enemy.health;
        healthBar.fillAmount = enemyhealth / maxhealth;

        if (enemyhealth <= 0)
        {


            this.gameObject.SetActive(false);

        }
    }
}

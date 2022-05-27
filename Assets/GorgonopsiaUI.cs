using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GorgonopsiaUI : MonoBehaviour
{
    public Image healthBar;
    GorgonopsiaStats enemy;
    public float enemyhealth;
    public float maxhealth;
    public GameObject Enemy;
    public GameObject EnemyHealthBar;
    public PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        enemy = Enemy.gameObject.GetComponent<GorgonopsiaStats>();
        maxhealth = enemy.health;
        //enemy.health = 150;
    }

    // Update is called once per frame
    void Update()
    {
        enemyhealth = enemy.health;
        healthBar.fillAmount = enemyhealth / maxhealth;

        if (playerStatus.playerdeath == true)
        {
            enemy.health = maxhealth;
        }

        if (enemyhealth <= 0)
        {


            this.gameObject.SetActive(false);

        }
    }
}

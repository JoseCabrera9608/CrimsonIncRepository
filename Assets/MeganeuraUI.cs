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
    public GameObject Player;
    public GameObject Enemy;
    public GameObject EnemyHealthBar;
    public PlayerStatus playerStatus;
    public bool playerdeath1;
    public bool findboss;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerStatus = Player.GetComponent<PlayerStatus>();
        Enemy = GameObject.FindGameObjectWithTag("Boss");
        enemy = Enemy.gameObject.GetComponent<MeganeuraStats>();
        maxhealth = enemy.health;
        //enemy.health = 150;
    }

    // Update is called once per frame
    void Update()
    {
        enemyhealth = enemy.health;
        healthBar.fillAmount = enemyhealth / maxhealth;

        playerdeath1 = playerStatus.playerdeath;

        if (playerStatus.playerdeath == true)
        {
            enemy.health = maxhealth;
            findboss = false;

            Enemy = GameObject.FindGameObjectWithTag("Boss");
            enemy = Enemy.gameObject.GetComponent<MeganeuraStats>();


        }

        if (enemyhealth <= 0)
        {


            EnemyHealthBar.SetActive(false);

        }
    }
}

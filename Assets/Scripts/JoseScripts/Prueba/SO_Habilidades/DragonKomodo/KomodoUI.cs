using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KomodoUI : MonoBehaviour
{
    public Image healthBar;
    KomodoController enemy;
    public int enemyhealth;
    public GameObject Enemy;
    public ParticleSystem deathParticles;

    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<KomodoController>();
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
            ParticleSystem temporalbullet = Instantiate(deathParticles);
            temporalbullet.transform.position = Enemy.transform.position;
            Destroy(Enemy);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MamutUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;
    EnemyMamut enemy;
    public int enemyhealth;
    public GameObject Enemy;
    public ParticleSystem deathParticles;
    public ProgressManager progress;

    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<EnemyMamut>();
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
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
            progress.level1 = true;
            SceneManager.LoadScene("Hub");
            Destroy(Enemy);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CalamarUI : MonoBehaviour
{
    public Image healthBar;
    CalamarController enemy;
    public int enemyhealth;
    public GameObject Enemy;
    public ParticleSystem deathParticles;
    public ProgressManager progress;

    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<CalamarController>();
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
            StartCoroutine(Muerte());
           
        }

    }
    IEnumerator Muerte()
    {
        yield return new WaitForSeconds(1.8f);
        ParticleSystem temporalbullet = Instantiate(deathParticles);
        temporalbullet.transform.position = Enemy.transform.position;
        progress.level3 = true;
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Hub");
        Destroy(Enemy);
    }
}


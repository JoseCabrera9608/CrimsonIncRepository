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
    public Fade fade;
    public GameObject Player;
    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<CalamarController>();
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        enemy.health = enemyhealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = enemyhealth / 170f;
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
        yield return new WaitForSeconds(4.8f);
        fade.Fadein();
        if (fade.fadeinend == true)
        {
            Destroy(Player);
            progress.lastposition = progress.hubpos;
            SceneManager.LoadScene("Hub");
            Destroy(Enemy);
        }


    }
}


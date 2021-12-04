using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class KomodoUI : MonoBehaviour
{
    public Image healthBar;
    KomodoController enemy;
    public int enemyhealth;
    public GameObject Enemy;
    public ParticleSystem deathParticles;
    public ProgressManager progress;
    public Fade fade;

    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<KomodoController>();
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
        enemy.health = enemyhealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = enemyhealth / 150f;
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
            progress.tutorial = true;
            //fade.Fadein();
            StartCoroutine(FadeEffect());

            if (fade.fadeinend == true)
            {
                SceneManager.LoadScene("Hub");
                Destroy(Enemy);
            }


        }
        IEnumerator FadeEffect()
        {
            yield return new WaitForSeconds(2f);
            fade.Fadein();
        }
    }
}

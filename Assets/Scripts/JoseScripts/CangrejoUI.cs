using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CangrejoUI : MonoBehaviour
{
    public Image healthBar;
    BossCangrejo enemy;
    public int enemyhealth;
    public GameObject Enemy;
    public ParticleSystem deathParticles;
    public ProgressManager progress;
    public Fade fade;
    public GameObject Player;

    void Start()
    {
        enemy = Enemy.gameObject.GetComponent<BossCangrejo>();
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Fade>();
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
            //ParticleSystem temporalbullet = Instantiate(deathParticles);
            //  temporalbullet.transform.position = Enemy.transform.position;
            // progress.tutorial = true;
            //fade.Fadein();
            Destroy(Enemy);
            StartCoroutine(FadeEffect());

            if (fade.fadeinend == true)
            {
                Destroy(Player);
                progress.lastposition = progress.hubpos;
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

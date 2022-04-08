using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBar;
    public PlayerStats player;
    //public PlayerMovement playermov;
    public int startHealth;
    public int healingcharges;
    public Image healingBar;

    public Image dashCD;
    public Sprite dashfull;
    public Sprite dashcharging;

    public bool getDamage;
    public Image bloodImage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //playermov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        startHealth = 100;
        healingcharges = 5;
        dashCD.sprite = dashfull;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = player.playerlife / 100f;
        healingBar.fillAmount = healingcharges / 5f;
        //dashCD.fillAmount = playermov.dashingcd / 2f;

        /*if (playermov.dashingcd >= 2f)
        {
            dashCD.sprite = dashfull;
        }
        else
        {
            dashCD.sprite = dashcharging;
        }*/

        if (Input.GetKeyDown(KeyCode.Q) && healingcharges >=1 && player.playerlife <100)
        {
            FindObjectOfType<AudioManager>().Play("Curacion");
            healingcharges -= 1;
            
            player.playerlife += 50;
            getDamage = true;
        }

        if (getDamage)
        {
            Color Opaque = new Color(1, 1, 1, 1);
            bloodImage.color = Color.Lerp(bloodImage.color, Opaque, 20 * Time.deltaTime);
            if (bloodImage.color.a >= 0.8) //Almost Opaque, close enough
            {
                getDamage = false;
            }
        }
        if (!getDamage)
        {
            Color Transparent = new Color(1, 1, 1, 0);
            bloodImage.color = Color.Lerp(bloodImage.color, Transparent, 20 * Time.deltaTime);
        }

    }


}

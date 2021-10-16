using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Image healthBar;
    public PlayerMovement player;
    public int startHealth;
    public int healingcharges;
    public Image healingBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        startHealth = 100;
        healingcharges = 5;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = player.playerlife / 100f;
        healingBar.fillAmount = healingcharges / 5f;

        if (Input.GetKeyDown(KeyCode.Q) && healingcharges >=1)
        {
            healingcharges -= 1;
            player.playerlife += 50;
        }

    }
}

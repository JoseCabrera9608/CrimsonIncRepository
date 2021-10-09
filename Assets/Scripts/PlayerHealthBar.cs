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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        startHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = player.playerlife / 100f;
    }
}

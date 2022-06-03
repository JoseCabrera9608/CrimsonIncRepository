using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEstacaBoss : MonoBehaviour
{
    public GameObject esferaStun;
    public float timer;
    bool activarEsfera;
    private void OnEnable()
    {
        PlayerStatus.onPlayerDeath += DestroyOnPlayerDeath;
    }
    private void OnDisable()
    {
        PlayerStatus.onPlayerDeath -= DestroyOnPlayerDeath;
    }

    private void DestroyOnPlayerDeath()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5)
        {
            activarEsfera = true;
        }

        if(activarEsfera == true)
        {
            esferaStun.SetActive(true);
            activarEsfera = false;
            timer = 0;
        }
        
    }

}

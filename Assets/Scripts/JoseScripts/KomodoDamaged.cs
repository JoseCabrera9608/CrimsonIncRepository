using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomodoDamaged : MonoBehaviour
{
    public GameObject enemy;
    private KomodoController boss;
    public PlayerAttack playerattack;
    // Start is called before the first frame update
    void Start()
    {
        playerattack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        boss = enemy.gameObject.GetComponent<KomodoController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            Debug.Log("Ga");

            if (other.gameObject.CompareTag("PlayerWeapon") && playerattack.attacking == true)
            {

                Debug.Log("PlayerContactTrigger");

                boss.hitted = true;

                /*if (timer >= 0.5)
                {
                    nupMesh.material = matHitted;
                    timer = 0;

                }*/
            }
        }
    }
}


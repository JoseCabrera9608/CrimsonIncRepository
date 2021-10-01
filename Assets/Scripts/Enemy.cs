using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerAttack player;
    public ParticleSystem deathParticles;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GAAATrigger");

        

        if (other.gameObject.CompareTag("PlayerWeapon") && player.attacking == true)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public ParticleSystem part;
    public bool colplayer;
    public float timer;

    public AgniBoss agniBoss;

    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        agniBoss = GetComponentInParent<AgniBoss>();

    }

    // Update is called once per frame
    void Update()
    {
        if (colplayer == true)
        {
            if (agniBoss.modoNitrogeno == true)
            {
                PlayerCollisionIce();
            }
            else
            {
                PlayerCollision();
            }
           
        }
        timer += Time.deltaTime;

        if (agniBoss.modoNitrogeno == true)
        {
            
        }

    }

    private void PlayerCollision()
    {
        if (timer >= 0.1f)
        {
            PlayerStatus.damagePlayer?.Invoke(3);
            colplayer = false;
            timer = 0;
        }

    }

    private void PlayerCollisionIce()
    {
        if (timer >= 0.1f)
        {
            PlayerStatus.damagePlayer?.Invoke(30);
            colplayer = false;
            timer = 0;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("gaaaprrooo");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            colplayer = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilRecto : MonoBehaviour
{
    public float speed;
    public float damage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        Destroy(this.gameObject, 3f);
    }

    /*  private void OnTriggerEnter(Collider other)
      {
          Debug.Log("CollisionoConCubo");
          Destroy(this.gameObject);
      }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.damagePlayer?.Invoke(damage);
            PlayerSingleton.Instance.playerHitted = true;
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Limite"))
        {
            Destroy(this.gameObject);
        }
    }

    
}

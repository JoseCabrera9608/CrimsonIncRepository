using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnce : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    void Start()
    {
        damage = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus.damagePlayer?.Invoke(damage);
            damage = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float lifetaim;
    public float timer;
    public float coldelay;

    public FireTrap fireTrap;
    public CapsuleCollider col;
    
    // Start is called before the first frame update
    void Start()
    {
        fireTrap = GameObject.FindGameObjectWithTag("Trampas").GetComponent<FireTrap>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        lifetaim = fireTrap.firelifetime;

        if (lifetaim < timer)
        {
            Destroy(gameObject);
        }

        if (timer > 1)
        {
            col.enabled = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP = -11;
        }
    }
}

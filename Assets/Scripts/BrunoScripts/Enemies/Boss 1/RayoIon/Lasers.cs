using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private MeganeuraBoss boss;
    [SerializeField]private bool OnRange;
    private BoxCollider col;
    private void Start()
    {
        boss = GetComponentInParent<MeganeuraBoss>();
        col = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        //if (OnRange)
        //{
        //    PlayerSingleton.Instance.playerCurrentHP -= boss.currentDamageValue * Time.deltaTime;
        //}

        if (col.enabled == false) OnRange = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("true on colision");
            OnRange = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("false on colision");
            OnRange = false;
        }
    }
}

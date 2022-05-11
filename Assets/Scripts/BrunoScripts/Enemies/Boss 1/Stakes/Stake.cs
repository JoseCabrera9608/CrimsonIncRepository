using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stake : MonoBehaviour
{
    private Rigidbody rb;
    private LayerMask groundLayer;
    private Collider _collider;
    private MeganeuraBoss boss;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundLayer = LayerMask.NameToLayer("Suelo");
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        boss = FindObjectOfType<MeganeuraBoss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;
            _collider.isTrigger = false;
        }

        if (other.CompareTag("PlayerWeapon")) Destroy(gameObject);

    }
}

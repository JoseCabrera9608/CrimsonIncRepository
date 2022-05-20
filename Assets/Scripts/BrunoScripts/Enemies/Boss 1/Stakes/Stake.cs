using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stake : MonoBehaviour
{
    private Rigidbody rb;
    private LayerMask groundLayer;
    private Collider _collider;
    private MeganeuraBoss boss;

    private bool collided = false;
    private float t;
    private float maxT;
    private LineRenderer lr;
    [HideInInspector]public Transform parent;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        groundLayer = LayerMask.NameToLayer("Suelo");
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        boss = FindObjectOfType<MeganeuraBoss>();
        maxT = 10;
    }
    private void Update()
    {
        if (collided == false) t += Time.deltaTime;
        if (t > maxT) Destroy(gameObject);

        SetLineRenderer();
    }
    private void SetLineRenderer()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, parent.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.isKinematic = true;
            _collider.isTrigger = false;
            collided = true;
        }

        if (other.CompareTag("PlayerWeapon")) Destroy(gameObject);

    }
}

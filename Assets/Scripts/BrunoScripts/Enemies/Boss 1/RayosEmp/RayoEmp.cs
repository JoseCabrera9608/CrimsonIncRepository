using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoEmp : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject target;
    [HideInInspector] public float speed;
    [HideInInspector] public float rotationSpeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float t;
    [HideInInspector] public float lifeTime;
    [HideInInspector] public float followTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerStatus>().gameObject;
    }
    private void Update()
    {
        t += Time.deltaTime;
        if (t <= followTime)
        {
            rb.velocity = transform.forward * speed;
            Quaternion direction = Quaternion.LookRotation((target.transform.position + new Vector3(0, 1, 0)) - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotationSpeed * Time.deltaTime);
        }
        else if (t >= lifeTime) Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("PlayerWeapon"))
        {
            PlayerStatus.damagePlayer?.Invoke(damage);
            Debug.Log("Current hp= " + PlayerSingleton.Instance.playerCurrentHP);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}

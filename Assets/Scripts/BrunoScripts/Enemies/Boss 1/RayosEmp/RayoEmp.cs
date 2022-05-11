using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayoEmp : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    public float damage;
    private float lifeTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = FindObjectOfType<PlayerStatus>().gameObject;
    }
    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime <= 20)
        {
            rb.velocity = transform.forward * speed;
            Quaternion direction = Quaternion.LookRotation((target.transform.position + new Vector3(0, 1, 0)) - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotationSpeed * Time.deltaTime);
        }
        else if (lifeTime >= 40) Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("PlayerWeapon"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
            Debug.Log("Current hp= " + PlayerSingleton.Instance.playerCurrentHP);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}

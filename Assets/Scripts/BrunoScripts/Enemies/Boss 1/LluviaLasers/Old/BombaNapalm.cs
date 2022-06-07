using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaNapalm : MonoBehaviour
{
    private Transform playerPosition;
    private Rigidbody rb;
    [HideInInspector] public float explotionDamage;
    [HideInInspector] public float burnDamage;
    private float distanceToPlayer;
    public float timeToExplote=3;
    public float burnTime=10;
    private float t;

    [SerializeField] private GameObject explotion;
    private bool touchedSomething;
    [SerializeField]private SphereCollider bombCollider;
    [SerializeField]private SphereCollider regularCollider;
    [SerializeField]private BoxCollider burnCollider;
    [SerializeField]private GameObject burnBox;
    private bool exploded;
    private bool playerInRange;
    void Start()
    {
        playerPosition = FindObjectOfType<PlayerStatus>().transform;
        distanceToPlayer = Vector3.Distance(transform.position, playerPosition.position);
        rb = GetComponent<Rigidbody>();
        AlignToPlayer();
        ApplyForce();
        rb.centerOfMass = Vector3.one;
    }
    private void Update()
    {
        Explode();
        BurnGround();
        DamagePlayer();
    }

    private void AlignToPlayer()
    {
        var lookPos = playerPosition.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        Vector3 direction = new Vector3(-10, 0, 0);
        transform.localEulerAngles += direction;
        
    }

    private void ApplyForce()
    {
        rb.velocity = transform.forward * (distanceToPlayer);
    }
    private void Explode()
    {
        if (touchedSomething) t += Time.deltaTime;

        if (t >= timeToExplote&&explotion.activeInHierarchy==false)
        {
            rb.isKinematic = true;
            transform.localEulerAngles = Vector3.zero;
            explotion.SetActive(true);
            bombCollider.enabled = true;
            regularCollider.enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            burnBox.SetActive(true);
            StartCoroutine(DeactivateTrigger());
        }
    }
    private void BurnGround()
    {
        if (exploded) burnCollider.enabled = true;

        if (t > burnTime) Destroy(gameObject);

    }
    private void DamagePlayer()
    {
        if (playerInRange && exploded) PlayerStatus.damagePlayer?.Invoke(burnDamage * Time.deltaTime);       
    }
    private IEnumerator DeactivateTrigger()
    {
        yield return new WaitForSeconds(.5f);
        explotion.GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        bombCollider.enabled = false;
        t = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        touchedSomething = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (exploded) playerInRange = true;
            else PlayerStatus.damagePlayer?.Invoke(explotionDamage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

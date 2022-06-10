using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaJaeger : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject fireArea;
    private GorgonopsiaStats stats;
    [Header("================CONTROL VARS=============")]
    public float speed;
    public float damage;
    public float timeToAct;
    //public float explotionRadius;
    public float distanceTreshHold;
    public float rotationSpeed;

    private bool onGround;
    private bool launched;
    private float distanceToTarget;
    private Vector3 target;
    void Start()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;
        stats = FindObjectOfType<GorgonopsiaStats>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(AutoDestroy());
    }

    void Update()
    {
        Explode2();
        TrackPlayer();
        ApplyForwardForce();
    }
    private void Explode2()
    {
        Vector3 target = player.transform.position + Vector3.up * 1.6f;
        distanceToTarget = Vector3.Distance(transform.position, target);

        if (distanceToTarget <= distanceTreshHold && launched)
        {
            GameObject obj = Instantiate(particles);
            obj.transform.position = transform.position;
            PlayerStatus.damagePlayer?.Invoke(damage);

            if (stats.fireBonus)
            {
                GameObject fire = Instantiate(fireArea);
                fire.transform.position = transform.position - new Vector3(0, transform.localScale.y / 2, 0);
                fire.transform.localScale = new Vector3(transform.localScale.x * 3, .3f, transform.localScale.z * 3);
            }
            Destroy(gameObject);
        }


    }
    private void TrackPlayer()
    {
        Vector3 directionPos = player.transform.position - transform.position;
        directionPos += Vector3.up * 1.6f;
        Quaternion direction = Quaternion.LookRotation(directionPos);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotationSpeed * Time.deltaTime);
    }
    private void ApplyForwardForce()
    {
        if (launched) rb.velocity = transform.forward * speed;
    }
    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
    private IEnumerator ApplyForce()
    {
        target = player.transform.position;
        yield return new WaitForSeconds(timeToAct);
        launched = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if (onGround == false)
            {
                StartCoroutine(ApplyForce());
                rb.AddForce(Vector3.up * 8, ForceMode.VelocityChange);
            }
            onGround = true;
        }
    }
}

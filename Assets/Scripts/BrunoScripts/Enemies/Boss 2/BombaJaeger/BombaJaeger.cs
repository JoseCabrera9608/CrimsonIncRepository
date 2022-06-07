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
    public float explotionRadius;
    public float distanceTreshHold;

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
        Explode();
    }
    private void Explode()
    {
        distanceToTarget = Vector3.Distance(target, transform.position);
        if (distanceToTarget <= distanceTreshHold&&launched)
        {
            GameObject obj = Instantiate(particles);
            obj.transform.position = transform.position;
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= distanceTreshHold)
            {
                //PlayerSingleton.Instance.playerCurrentHP -= damage;
                PlayerStatus.damagePlayer?.Invoke(damage);
            }

            if (stats.fireBonus)
            {
                GameObject fire = Instantiate(fireArea);
                fire.transform.position = transform.position-new Vector3(0,transform.localScale.y/2,0);
                fire.transform.localScale =new Vector3( transform.localScale.x*3,.3f,transform.localScale.z*3);
            }
            Destroy(gameObject);
        }
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
        Vector3 direction = target- transform.position;
        rb.AddForce(direction.normalized*speed, ForceMode.VelocityChange);
        launched = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            if (onGround == false)
            {
                StartCoroutine(ApplyForce());
                rb.AddForce(Vector3.up * 5,ForceMode.VelocityChange);
            }
            onGround = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, explotionRadius);
    }
}

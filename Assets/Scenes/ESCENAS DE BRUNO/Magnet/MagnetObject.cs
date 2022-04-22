using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class MagnetObject : MonoBehaviour
{
    public List<Rigidbody> objectsToAtract;
    [SerializeField] private Transform pullPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private ParticleSystem particles;

    [Header("Settings")]
    [SerializeField] private bool showAtractionRadius;
    [SerializeField] private float atractionForce;
    [SerializeField] private float atractionRadius;
    [SerializeField] private float atractionDuration;
    [SerializeField] private float atractionDelay;
    [SerializeField] private bool isActive;
    [SerializeField]private float t;

    void Start()
    {
        objectsToAtract = new List<Rigidbody>();
        t = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        StartAtraction();
        PullPlayer();
        PullDebris();
    }
    private IEnumerator RestartAtraction()
    {
        yield return new WaitForSeconds(atractionDuration);
        isActive = false;
        t = 0;
        particles.Stop();
        ExplotionAtEndUwu();
    }
    private void StartAtraction()
    {
        
        if (!isActive)
        {
            t += Time.deltaTime;
            if (t >= atractionDelay / 2) particles.Play();
            if (t >= atractionDelay)
            {
                Debug.Log("Iniciando");
                isActive = true;
                //particles.Play();
                StartCoroutine(RestartAtraction());
            }
        }
    }
    private void PullPlayer()
    {
        if (player == null||isActive==false) return;

        Vector3 pullDirection = pullPoint.position - player.transform.position;
        Vector3 pullDirection2 = player.transform.position - pullPoint.position;
        //pullDirection.y = player.transform.position.y;
        Debug.DrawRay(transform.position,pullDirection2);
        Rigidbody rb = player.GetComponent<Rigidbody>();

        rb.AddForce(pullDirection*atractionForce*Time.fixedDeltaTime, ForceMode.Force);
    }
    
    private void PullDebris()
    {
        if (isActive==false) return;
        foreach(Rigidbody rb in objectsToAtract)
        {
            Vector3 pullDirection = pullPoint.position - rb.position;
            pullDirection.y = rb.position.y;
            rb.AddForce(pullDirection * atractionForce * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
    private void ExplotionAtEndUwu()
    {
        foreach (Rigidbody rb in objectsToAtract)
        {
            rb.AddExplosionForce(1000, transform.position, 10);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) player = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) player = null;
    }
    private void OnDrawGizmos()
    {
        GetComponent<SphereCollider>().radius = atractionRadius;
        Gizmos.color = Color.red;
        
        if(showAtractionRadius) Gizmos.DrawWireSphere(pullPoint.position,
            GetComponent<SphereCollider>().radius);

        atractionRadius = particles.shape.radius;
        atractionDuration = particles.main.duration * 20;
    }

}

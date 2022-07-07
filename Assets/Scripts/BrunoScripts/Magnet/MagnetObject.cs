using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MagnetObject : MonoBehaviour
{
    public List<Rigidbody> objectsToAtract;
    [SerializeField] private Transform pullPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pullSphere;
    public bool playerOnRange;
    [Header("Settings")]
    [SerializeField] private bool showAtractionRadius;
    [SerializeField] private float atractionForce;
    [SerializeField] private float atractionRadius;
     private float atractionHolder;
    [SerializeField] private float atractionDuration;
    [SerializeField] private float atractionDelay;
    [SerializeField] private bool isActive;
    [SerializeField]private float t;

    void Start()
    {
        objectsToAtract = new List<Rigidbody>();
        atractionHolder = atractionRadius;
        t = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartAtraction();
        PullPlayer();
        PullDebris();

        atractionRadius = pullSphere.transform.localScale.x / 2;
    }
    private IEnumerator RestartAtraction()
    {
        yield return new WaitForSeconds(atractionDuration);
        isActive = false;
        t = 0;
        
    }
    private void StartAtraction()
    {
        
        if (!isActive)
        {
            t += Time.deltaTime;
            if (t >= atractionDelay / 2 && pullSphere.transform.localScale == Vector3.zero)
            {
                pullSphere.transform.DOScale(atractionHolder * 2, 1).SetEase(Ease.OutBack);
            }
            if (t >= atractionDelay)
            {
                isActive = true;
                pullSphere.transform.DOScale(0, atractionDuration).SetEase(Ease.InCubic);
                StartCoroutine(RestartAtraction());
            }
        }
    }
    private void PullPlayer()
    {
        if (player == null || isActive == false)
        {
            playerOnRange = false;
            return;
        }
        else
        {
            playerOnRange = true;
            Vector3 pullDirection = pullPoint.position - player.transform.position;
            Vector3 pullDirection2 = player.transform.position - pullPoint.position;
            //pullDirection.y = player.transform.position.y;
            Debug.DrawRay(transform.position, pullDirection2);
            Rigidbody rb = player.GetComponent<Rigidbody>();

            rb.AddForce(pullDirection * atractionForce * Time.fixedDeltaTime, ForceMode.Force);
        }       
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

       
    }

}

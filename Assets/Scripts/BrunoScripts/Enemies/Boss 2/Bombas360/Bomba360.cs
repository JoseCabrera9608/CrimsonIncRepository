using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bomba360 : MonoBehaviour
{
    [SerializeField] private GameObject border1;
    [SerializeField] private GameObject border2;
    [SerializeField] private GameObject feedback;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject particles;
    private float initialDistanceToPlayer;
    [SerializeField] private float distanceToPlayer;

    [SerializeField] private float minDistanceToDamage;
    [SerializeField] private float maxDistanceToDamage;
    //VARIABLES DE STATS
    public float distanceTreshold;
    public float timeToDamage;
    public float damage;
    void Start()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        initialDistanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        StartCoroutine(ChargeAttack());
        SetBorders();
        SetDamageDistances();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    }
    private IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(timeToDamage);
        GameObject obj = Instantiate(particles);
        obj.transform.position = transform.position;
        float value = initialDistanceToPlayer * 2;
        obj.transform.localScale = new Vector3(value, value, value / 3);

        GorgonopsiaSFX.Instance.Play("bomba360");
        //damage to player
        if (distanceToPlayer >= minDistanceToDamage && distanceToPlayer <= maxDistanceToDamage) DamagePlayer();

        Destroy(gameObject);

    }
    private void SetFeedBack()
    {
        GameObject donut = Instantiate(feedback);

        StartCoroutine(DestroyFeedback(donut));
        donut.transform.position = transform.position;
        donut.transform.parent = transform;
        //donut.transform.localScale = new Vector3(distanceToPlayer+(distanceTreshold/2)- 1.48685f, 1, distanceToPlayer + (distanceTreshold / 2)- 1.48685f);
        donut.transform.localScale = new Vector3((distanceToPlayer * 2 + distanceTreshold / 1.5f) / 10, 1, (distanceToPlayer * 2 + distanceTreshold / 1.5f) / 10);
        donut.GetComponent<MeshRenderer>().material.DOFade(1, timeToDamage);
    }
    private IEnumerator DestroyFeedback(GameObject obj)
    {
        yield return new WaitForSeconds(timeToDamage);
        Destroy(obj);

    }
    private void DamagePlayer()
    {
        PlayerStatus.damagePlayer?.Invoke(damage);
    }
    private void SetDamageDistances()
    {
        minDistanceToDamage = distanceToPlayer - distanceTreshold / 2;
        maxDistanceToDamage = distanceToPlayer + distanceTreshold / 2;
    }
    private void SetBorders()
    {
        float value1 = distanceToPlayer * 2 - distanceTreshold;
        float value2 = distanceToPlayer * 2 + distanceTreshold;
        border1.transform.localScale = new Vector3(value1 / 10, 1, value1 / 10);
        border2.transform.localScale = new Vector3(value2 / 10, 1, value2 / 10);
        SetFeedBack();
        //distance *2 + treshhold
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + transform.forward * minDistanceToDamage, transform.position + transform.forward * maxDistanceToDamage);
    }
}
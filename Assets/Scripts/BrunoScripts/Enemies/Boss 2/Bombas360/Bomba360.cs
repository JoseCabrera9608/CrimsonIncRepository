using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba360 : MonoBehaviour
{
    [SerializeField]private GameObject border1;
    [SerializeField] private GameObject border2;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject particles;
    private float initialDistanceToPlayer;
    private float distanceToPlayer;

    private float minDistanceToDamage;
    private float maxDistanceToDamage;
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
        obj.transform.localScale = new Vector3(value,value,value/3);
        //damage to player
        if (distanceToPlayer >= minDistanceToDamage && distanceToPlayer <= maxDistanceToDamage) DamagePlayer();

        Destroy(gameObject);
        
    }
    private void DamagePlayer()
    {
        PlayerStatus.damagePlayer?.Invoke(damage);
    }
    private void SetDamageDistances()
    {
        minDistanceToDamage = distanceToPlayer - distanceTreshold;
        maxDistanceToDamage = distanceToPlayer + distanceTreshold;
    }
    private void SetBorders()
    {
        float value1 = distanceToPlayer*2 - distanceTreshold;
        float value2 = distanceToPlayer*2 + distanceTreshold;
        border1.transform.localScale = new Vector3(value1/10,1, value1/10);
        border2.transform.localScale = new Vector3(value2/10,1, value2/10);

    }
}

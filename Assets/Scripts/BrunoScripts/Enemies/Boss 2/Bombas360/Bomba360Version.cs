using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bomba360Version : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject particles;
    [SerializeField] private MeshCollider col;
    [SerializeField] private float distanceToPlayer;
    //VARIABLES DE STATS
    public float distanceTreshold;
    public float timeToDamage;
    public float damage;
    void Start()
    {
        OnSpawn();
    }
    private void OnSpawn()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > 3) transform.localScale = Vector3.one * (distanceToPlayer + distanceTreshold * .4f);
        else transform.localScale = Vector3.one * distanceToPlayer;
        GetComponent<MeshRenderer>().material.DOFade(1, timeToDamage * .85f);
        StartCoroutine(ChargeAttack(timeToDamage));
    }
    
    private IEnumerator ChargeAttack(float time)
    {
        yield return new WaitForSeconds(time);
        col.enabled = true;
        StartCoroutine(AutoDestroy(.1f));
        GorgonopsiaSFX.Instance.Play("bomba360");

        GameObject obj = Instantiate(particles);
        obj.transform.position = transform.position;
        float value = distanceToPlayer * 2;
        obj.transform.localScale = new Vector3(value, value, value / 3);
    }
    private IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == player)
        {
            PlayerStatus.damagePlayer?.Invoke(damage);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}

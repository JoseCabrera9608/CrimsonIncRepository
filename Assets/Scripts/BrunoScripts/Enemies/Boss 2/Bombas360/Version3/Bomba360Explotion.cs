using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bomba360Explotion : MonoBehaviour
{
    public GameObject particles;
    public float timeToDamage;
    public float damage;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.DOFade(1, timeToDamage);
        StartCoroutine(Charge(timeToDamage));
    }

    private IEnumerator Charge(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = false;
        GameObject obj = Instantiate(particles);
        obj.transform.position = transform.position + new Vector3(0, 1, 0);
        obj.transform.localScale = transform.localScale;
        StartCoroutine(AutoDestroy(.1f));
    }
    private IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus.damagePlayer?.Invoke(damage);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}

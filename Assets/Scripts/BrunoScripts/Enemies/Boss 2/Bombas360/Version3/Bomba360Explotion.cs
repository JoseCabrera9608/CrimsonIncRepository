using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Bomba360Explotion : MonoBehaviour
{
    public GameObject particles;
    public GameObject mesh;
    public float endScale;
    public float timeToDamage;
    public float damage;

    private void Start()
    {
        mesh.transform.DOScale(new Vector3(endScale*2,0.1f,endScale*2),timeToDamage);
        StartCoroutine(Charge(timeToDamage));
    }

    private IEnumerator Charge(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<SphereCollider>().enabled = true;
        mesh.GetComponent<MeshRenderer>().enabled = false;
        GameObject obj = Instantiate(particles);
        obj.transform.position = transform.position + new Vector3(0, 1, 0);
        obj.transform.localScale = Vector3.one*endScale*2;
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

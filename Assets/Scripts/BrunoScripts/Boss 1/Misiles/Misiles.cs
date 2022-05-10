using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Misiles : MonoBehaviour
{
    public GameObject full;
    public GameObject border;
    public float timer;
    public float damage;
    public float explotionDuration;
    void Start()
    {
        full.transform.DOScale(1, timer).OnComplete(CreateExplosion);
    }
    private void CreateExplosion()
    {
        border.GetComponent<MeshRenderer>().enabled = false;
        full.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DestroyObject());
        GetComponent<SphereCollider>().enabled = true;
    }
    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(explotionDuration);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
        }
    }
}

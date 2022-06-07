using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    private GorgonopsiaStats stats;
    private float damage;
    private bool playerOnRange;
    void Start()
    {
        stats = FindObjectOfType<GorgonopsiaStats>();
        damage = stats.fireDamage;
        StartCoroutine(AutoDestroy(stats.fireDuration));
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOnRange) /*PlayerSingleton.Instance.playerCurrentHP -= damage * Time.deltaTime*/ PlayerStatus.damagePlayer?.Invoke(damage*Time.deltaTime);
    }
    private IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerOnRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerOnRange = false;
    }
}

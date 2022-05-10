using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloElectrificado : MonoBehaviour
{
    [SerializeField] float time;
    public BoxCollider colliderSuelo;
    public float damage;


    void Start()
    {
        colliderSuelo = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 2)
        {
            StartCoroutine(ActivarCollider());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
            Debug.Log("Daño por piso");
        }
    }


    IEnumerator ActivarCollider()
    {
        colliderSuelo.enabled = true;
        yield return new WaitForSeconds(0.58f);
        colliderSuelo.enabled = false;
        time = 0;
    }

    
}

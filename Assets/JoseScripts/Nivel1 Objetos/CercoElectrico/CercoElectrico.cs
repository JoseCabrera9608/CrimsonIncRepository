using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercoElectrico : MonoBehaviour
{
    public float damage;
    [SerializeField] bool desactivar;
    Animator anim;
    BoxCollider collider;
    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (desactivar == true)
        {
            anim.SetTrigger("Apagar");
            collider.enabled = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
        }
    }
}

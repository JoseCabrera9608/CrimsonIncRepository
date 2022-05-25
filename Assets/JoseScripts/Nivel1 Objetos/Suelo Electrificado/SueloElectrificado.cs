using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloElectrificado : MonoBehaviour
{
    [SerializeField] float time;
    public BoxCollider colliderSuelo;
    public float damage;
    public bool desactivado;
    public int id;

    void Start()
    {
        colliderSuelo = GetComponent<BoxCollider>();
        BossGameEVent.current.Conexion += Desactivar;

    }

    // Update is called once per frame
    void FixedUpdate()
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
            PlayerSingleton.Instance.playerHitted = true;
            Debug.Log("Daño por piso");
        }
    }


    IEnumerator ActivarCollider()
    {
        colliderSuelo.enabled = true;
        yield return new WaitForSeconds(0.55f);
        colliderSuelo.enabled = false;
        time = 0;
    }

    public void Desactivar (int id)
    {
        if(id == this.id)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        BossGameEVent.current.Conexion -= Desactivar;
    }

}

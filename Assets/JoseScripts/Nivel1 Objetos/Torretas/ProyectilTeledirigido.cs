using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilTeledirigido : MonoBehaviour
{
    public float speed;
    Transform target;
    GameObject cabezaPlayer;
    public float damage;
    void Start()
    {
        cabezaPlayer = GameObject.Find("PlayerHead");
        target = cabezaPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Teledirigido());
        Destroy(this.gameObject, 5f);
    }

    IEnumerator Teledirigido()
    {
        gameObject.transform.LookAt(target);
        transform.Translate(0f, 0f, speed * Time.deltaTime);
        yield return new WaitForSeconds(3.5f);
        target = null;
        transform.position += transform.forward * (speed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
            Destroy(this.gameObject);
        }
    }
}

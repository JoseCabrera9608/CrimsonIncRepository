using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bomba;
    public GameObject bombafirePoint;
    public float timer;
    public float cd;
    public bool bombafija;

    void Start()
    {
        timer = cd;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if (bombafija == true)
        {
            SpawnDeBomba();
        }
    }

    public void SpawnDeBomba()
    {
        GameObject tiposDisparo;
        if (bombafirePoint != null && timer >= cd)
        {

            tiposDisparo = Instantiate(bomba, bombafirePoint.transform.position, Quaternion.identity);
            tiposDisparo.transform.localRotation = this.gameObject.transform.rotation;
            timer = 0;
        }
    }

    public void LanzarBomba()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject tiposDisparo;

        if (!collision.gameObject.CompareTag("Enemy") && bombafija == false)
        {
            tiposDisparo = Instantiate(bomba, bombafirePoint.transform.position, Quaternion.identity);
            tiposDisparo.transform.localRotation = this.gameObject.transform.rotation;
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDaño : MonoBehaviour
{
    PlayerStats player;
    BoxCollider nubeCollider;
    float nube_ScaleX, nube_ScaleY, nube_ScaleZ;
    void Start()
    {
        player = GetComponent<PlayerStats>();
        nubeCollider = GetComponent<BoxCollider>();
        nube_ScaleX = 1f;
        nube_ScaleY = 1f;
        nube_ScaleZ = 1f;
        nubeCollider.size = new Vector3(nube_ScaleX, nube_ScaleY, nube_ScaleZ);
    }

    // Update is called once per frame
    void Update()
    {
        //nubeCollider.size += new Vector3(3, 3, 3) * Time.deltaTime;
        StartCoroutine(EscalarCollider());
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //player.playerlife -= 3.5f;
            Debug.Log("Daño por Nube");
        }
    }

    IEnumerator EscalarCollider()
    {
        nubeCollider.size += new Vector3(5, 1, 0.5f) * Time.deltaTime;
        yield return new WaitForSeconds(8.5f);
        nubeCollider.size = new Vector3(nube_ScaleX, nube_ScaleY, nube_ScaleZ);
    }
    
}

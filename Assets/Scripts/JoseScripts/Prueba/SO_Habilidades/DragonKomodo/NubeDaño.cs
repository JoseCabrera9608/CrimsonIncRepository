using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDaño : MonoBehaviour
{
    GameObject playerObj;
    PlayerStats player;
    BoxCollider nubeCollider;
    float nube_ScaleX, nube_ScaleY, nube_ScaleZ;
    bool atacando;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.gameObject.GetComponent<PlayerStats>();  
        /*nubeCollider = GetComponent<BoxCollider>();
        nube_ScaleX = 1f;
        nube_ScaleY = 1f;
        nube_ScaleZ = 1f;
        nubeCollider.size = new Vector3(nube_ScaleX, nube_ScaleY, nube_ScaleZ);*/
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //StartCoroutine(EscalarCollider());
       
            
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.playerlife -= 2;
            Debug.Log("Daño por Nube");
        }
    }

    IEnumerator EscalarCollider()
    {
        nubeCollider.size += new Vector3(2.8f, 1, 0.5f) * Time.deltaTime;
        yield return new WaitForSeconds(5f);
        nubeCollider.size -= new Vector3(5f, 2, 1f) * Time.deltaTime;
        yield return new WaitForSeconds(3.5f);
        atacando = false;
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NubeDaño : MonoBehaviour
{
    GameObject playerObj;
    PlayerStats player;
    BoxCollider nubeCollider;
    public CapsuleCollider capsuleCollider;
    float nube_ScaleX, nube_ScaleY, nube_ScaleZ;
    bool atacando;
    public bool activado = false;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.gameObject.GetComponent<PlayerStats>();
        //nubeCollider = GetComponent<BoxCollider>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        /*nube_ScaleX = 1f;
        nube_ScaleY = 1f;
        nube_ScaleZ = 1f;
        nubeCollider.size = new Vector3(nube_ScaleX, nube_ScaleY, nube_ScaleZ);*/
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(activado == true)
        {
            StartCoroutine(EscalarCollider());
       
        }
      
            
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.playerlife -= 1.1f;
            Debug.Log("Daño por Nube");
        }
    }

    IEnumerator EscalarCollider()
    {

        capsuleCollider.height += 5.3f * Time.deltaTime;
        if (capsuleCollider.height >= 25.92f)
        {
            capsuleCollider.height = 26;
            yield return new WaitForSeconds(7f);
            capsuleCollider.height = 1;
        }
       
    }
    

}

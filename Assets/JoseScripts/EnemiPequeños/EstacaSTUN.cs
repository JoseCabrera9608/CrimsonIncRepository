using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstacaSTUN : MonoBehaviour
{
    public GameObject areaStun;
    
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplotarEstaca());
    }

    // Update is called once per frame
  

    IEnumerator ExplotarEstaca()
    {
        yield return new WaitForSeconds(3f);
        areaStun.SetActive(true);
        yield return new WaitForSeconds(3.3f);
        Destroy(this.gameObject);


    }


    
}

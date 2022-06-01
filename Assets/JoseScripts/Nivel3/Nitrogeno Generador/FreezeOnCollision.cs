using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnCollision : MonoBehaviour
{
    public GameObject playerObject;
    SkinnedMeshRenderer meshPlayer;
    public Material frozenShader;
    float iceSliderMover;
    

    void Start()
    {
        
        meshPlayer = playerObject.GetComponent<SkinnedMeshRenderer>();
        
    }

    private void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            meshPlayer.material = frozenShader;

        }
    }

  

}

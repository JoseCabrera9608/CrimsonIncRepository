using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnCollision : MonoBehaviour
{
    public GameObject playerObject;
    public SkinnedMeshRenderer meshPlayer;
    public Material frozenShader;
    float iceSliderMover;

    

    void Start()
    {

        playerObject = GameObject.FindGameObjectWithTag("Player");
        meshPlayer = playerObject.GetComponentInChildren<SkinnedMeshRenderer>();
        
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

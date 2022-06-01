using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnCollision : MonoBehaviour
{
    public GameObject playerObject;
    SkinnedMeshRenderer meshPlayer;
    public Material frozenShader;
    public bool freezing;
    float iceSliderMover;
    

    void Start()
    {
        
        meshPlayer = playerObject.GetComponent<SkinnedMeshRenderer>();
        
        iceSliderMover = 0;
    }

    private void Update()
    {
        iceSliderMover += Time.deltaTime;
        if(freezing == true)
        {
            frozenShader.SetFloat("IceSlider", iceSliderMover);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        freezing = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            meshPlayer.material = frozenShader;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        freezing = false;
    }

}

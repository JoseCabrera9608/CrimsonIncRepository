using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator anim;
    public bool bossDoor;
    public BoxCollider triggerbox;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        anim.SetBool("CloseDoor", false);
        anim.SetBool("OpenDoor", true);
    }

    public void CloseDoor()
    {
        anim.SetBool("OpenDoor", false);
        anim.SetBool("CloseDoor", true);
    }

    public void BlockDoor()
    {
        if (bossDoor == true)
        {
            triggerbox.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CloseDoor();
            BlockDoor();

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScript : MonoBehaviour
{

    public GameObject Object;
    public bool playercol;
    public DialogueTrigger dialogue;
    public bool stopCollider;

    
    // Start is called before the first frame update
    void Start()
    {
        dialogue = this.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Object.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && playercol == false && stopCollider == false)
        {
            dialogue.StartDialogue();
            playercol = true;
            //Activate();
        }
        if (other.gameObject.CompareTag("Player") && playercol == false && stopCollider == true)
        {
            dialogue.StopDialogue();
            playercol = true;
            //Activate();
        }
    }
}

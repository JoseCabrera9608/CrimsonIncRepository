using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScript : MonoBehaviour
{

    public GameObject Object;
    public bool playercol;
    public DialogueTrigger dialogue;

    
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
        if (other.gameObject.CompareTag("Player") && playercol == false)
        {
            dialogue.StartDialogue();
            playercol = true;
            //Activate();
        }
    }
}

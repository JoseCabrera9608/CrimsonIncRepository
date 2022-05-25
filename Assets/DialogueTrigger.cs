using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }
    public void StopDialogue()
    {
        FindObjectOfType<DialogueManager>().CloseDialogue();
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
    public AudioClip audioClip;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}

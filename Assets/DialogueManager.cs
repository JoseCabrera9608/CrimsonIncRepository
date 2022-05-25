using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    private string sentence;

    //public AudioClip audioClip;
    public AudioSource audioSource;

    public float textspeed;


    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;
    
    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        backgroundBox.localScale = new Vector3(1, 1, 1);
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("ESTAS CONVERSANDO PRRO, HAS CARGADO: " + messages.Length);
        DisplayMessage();
    }

    public void CloseDialogue()
    {
        backgroundBox.localScale = new Vector3(1, 1, 1);
        isActive = false;
        backgroundBox.localScale = new Vector3(0, 0, 0);

        Debug.Log("Cerraste dialogo prro");
        audioSource.Stop();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        //messageText.text = messageToDisplay.message;
        StopAllCoroutines();

        sentence = messageToDisplay.message;
        audioSource.clip = messageToDisplay.audioClip;
        audioSource.Play();
        StartCoroutine(TypeSentence(sentence));


        Actor actorToDisplay = currentActors[messageToDisplay.actorId];

        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
        //audioClip = messageToDisplay.audioClip;
    }

    IEnumerator TypeSentence(string sentence)
    {
        messageText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            messageText.text += letter;
            yield return new WaitForSeconds(0.1f / textspeed);
        }


    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
            //audioSource.Play();
        }
        else
        {
            Debug.Log("ACABASTE DE HABLAR PRRO");
            isActive = false;
            backgroundBox.localScale = new Vector3(0, 0, 0);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActive == true)
        {
            //NextMessage();
            if (messageText.text == sentence)
            {
                audioSource.Stop();
                NextMessage();
            }
            else
            {
                StopAllCoroutines();
                messageText.text = sentence;
            }
        }
    }
    
}

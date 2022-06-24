using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Door : MonoBehaviour
{

    public Animator anim;
    public bool bossDoor;
    public BoxCollider triggerbox;
    public int enemysToOpen;
    public int consolas;
    public TextMeshPro text;
    public TextMeshPro text2;

    public ProgressManager progress;
    
    
    // Start is called before the first frame update
    void Start()
    {
        progress = GameObject.FindGameObjectWithTag("Progress").GetComponent<ProgressManager>();
        //text = GetComponentInChildren<TextMeshPro>();
        if (text != null) text.text = "Enemigos derrotados: 0/"+ enemysToOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if (text !=null)  text.text = "Enemigos derrotados: "+ progress.enemysdeath +"/" + enemysToOpen;
        if (text2 != null) text2.text = "Consolas Activadas: " + consolas + "/" + 1;


        if (progress.enemysdeath >= enemysToOpen && enemysToOpen != 0 && consolas ==1)
        {
            if (text != null) text.enabled = false;
            if (text2 != null) text.enabled = false;
            OpenDoor();
            progress.enemysdeath = 0;
        }
    }

    public void OpenDoor()
    {
        anim.SetBool("CloseDoor", false);
        anim.SetBool("OpenDoor", true);
    }

    public void CloseDoor()
    {
        progress.enemysdeath = 0;
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
            if (enemysToOpen == 0)
            {
                OpenDoor();
            }

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

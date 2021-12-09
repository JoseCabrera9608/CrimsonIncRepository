using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField]
    bool playerOnActivationRange;

    public bool elevatorReady;

    public float topYLimit;
    public float bottomYLimit;
    public bool elevatorGoingUp;
    public float elevatorSpeed;
    public float timer;
    public float soundtimer;
    public GameObject elevator;
    public Rigidbody elevatorRb;
    public BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        elevatorReady = true;
        playerOnActivationRange = false;
        elevatorRb = elevator.GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();

        if (elevatorGoingUp)
        {
            bottomYLimit = elevator.transform.position.y;
        }
        else
        {
            topYLimit = elevator.transform.position.y;

        }
    }

    // Update is called once per frame
    void Update()
    {
        InputCheck();
        MoveElevator();
        LimitCheck();

        if (elevatorRb.velocity.y == 0)
        {
            box.enabled = true;
            //FindObjectOfType<AudioManager>().Stop("Ascensor");
        }
        else
        {
            box.enabled = false;

        }

        soundtimer += Time.deltaTime;

        if (soundtimer >= 15)
        {
            FindObjectOfType<AudioManager>().Stop("Ascensor");
            soundtimer = 0;
        }


    }

    void InputCheck()
    {
        if (playerOnActivationRange && Input.GetKeyDown(KeyCode.E) && elevatorReady)
        {
            elevatorReady = false;
            soundtimer = 0;
            FindObjectOfType<AudioManager>().Play("Ascensor");
        }
    }

    void MoveElevator()
    {
        if (!elevatorReady)
        {

            if (elevatorGoingUp)
            {
                elevatorRb.velocity = new Vector3(0, elevatorSpeed, 0);
            }
            else
            {
                elevatorRb.velocity = new Vector3(0, -elevatorSpeed, 0);
                Debug.Log("Brrr el elevador baja");
            }

        }
    }

    void LimitCheck()
    {
        if (elevatorGoingUp)
        {




            if (elevator.transform.position.y >= topYLimit)
            {
                timer += Time.deltaTime;
                elevatorRb.velocity = new Vector3(0, 0, 0);

                if (timer >= 2)
                {
                    elevatorReady = false;
                    elevatorGoingUp = false;

                    timer = 0;
                }


            }
        }
        else
        {
            if (elevator.transform.position.y <= bottomYLimit)
            {

                timer += Time.deltaTime;
                elevatorRb.velocity = new Vector3(0, 0, 0);

                if (timer >= 2)
                {
                    elevatorReady = true;
                    elevatorGoingUp = true;

                    timer = 0;
                }


            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnActivationRange = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnActivationRange = false;
        }

    }
}

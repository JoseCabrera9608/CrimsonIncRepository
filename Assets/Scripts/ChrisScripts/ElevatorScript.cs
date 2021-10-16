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
    public GameObject elevator;
    public Rigidbody elevatorRb;
    // Start is called before the first frame update
    void Start()
    {
        elevatorReady = true;
        playerOnActivationRange = false;
        elevatorRb = elevator.GetComponent<Rigidbody>();

        if(elevatorGoingUp)
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
    }

    void InputCheck()
    {
        if (playerOnActivationRange && Input.GetKeyDown(KeyCode.E) && elevatorReady)
        {     
                elevatorReady = false; 
        }
    }

    void MoveElevator()
    {
        if(!elevatorReady)
        {
            
            if(elevatorGoingUp)
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
        if(elevatorGoingUp)
        {
            if(elevator.transform.position.y>=topYLimit)
            {
                elevatorReady = true;
                elevatorGoingUp = false;
                elevatorRb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            if(elevator.transform.position.y<=bottomYLimit)
            {
                elevatorReady = true;
                elevatorGoingUp = true;
                elevatorRb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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

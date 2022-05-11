using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOG : MonoBehaviour
{
    private Vector2 playerInput;
    private Vector3 moveDirection;
    private Rigidbody rb;
    //======================Una vez los valores esten definidos quitar y asignar en Singleton=====================
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movSpeed;
    //======================Una vez los valores esten definidos quitar y asignar en Singleton=====================

    [SerializeField] private bool isGrounded;
    private float airTime;
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallSpeed;
    [SerializeField] private float rayCastOffset;
    [SerializeField] LayerMask groundLayer;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //=====QUITAR LUEGO==========
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
   
    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
        Falling();
    }
    private void PlayerMovement()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //moveDirection = Camera.main.transform.forward * playerInput.y;
        moveDirection = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z) * playerInput.y;
        moveDirection += Camera.main.transform.right * playerInput.x;
        moveDirection.Normalize();

        moveDirection.y = 0;
        moveDirection *= movSpeed;

        Vector3 movementVelocity = moveDirection;
        rb.velocity = movementVelocity;
    }

    private void PlayerRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = Camera.main.transform.forward * playerInput.y;
        targetDirection += Camera.main.transform.right * playerInput.x;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero) targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void Falling()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        //Vector3 targetPosition;
        //targetPosition = transform.position;
        if (!isGrounded)
        {
            airTime += Time.deltaTime;
            //rb.AddForce(transform.forward * leapingVelocity);
            rb.AddForce(-Vector3.up * fallSpeed * airTime);
        }

        if (Physics.Raycast(rayCastOrigin,-Vector3.up,out hit, rayCastOffset,groundLayer))
        {
            // if(!isGrounded) asignar animacion de aterrizar

            Vector3 rayCastHitPoint = hit.point;
            //targetPosition.y = rayCastHitPoint.y + rayCastOffset;

            airTime = 0;
            isGrounded = true;
            
        }
        else
        {
            isGrounded = false;
        }

        //if (isGrounded)
        //{
        //    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
        //}
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position,-Vector3.up*rayCastOffset,Color.red);
        
    }
    

}

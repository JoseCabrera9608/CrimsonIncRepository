using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DiscosEmp : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject player;
    [HideInInspector]public float rotationSpeed;
    public float speed;
    [HideInInspector] public float maxSpeed;
    [HideInInspector] public float damage;
     public float staminaDamage;

    [HideInInspector] public bool left;
    [HideInInspector] public bool targetLocked;
    [HideInInspector] public float extraFollowTime;

     private bool canFollow=true;
     private float followT;

    [SerializeField] private SphereCollider col;
    [SerializeField] private GameObject graphics;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerStatus>().gameObject;
        speed = maxSpeed;
        AlignToPlayer();
        graphics.transform.DORotate(new Vector3(0, 360, 0), 0.5f, RotateMode.LocalAxisAdd).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        RotateToTarget();
        ApplyForwardForce();
        LockTarget();
        ExtraFollowTime();
        
    }
    private void ApplyForwardForce()
    {
        rb.velocity = transform.forward * speed;
        if (targetLocked == false)
        {
            rotationSpeed += Time.deltaTime * 20;
            speed -= Time.deltaTime*5f;
        }
        else if(targetLocked&&speed<maxSpeed)
        {
            speed += Time.deltaTime * 6f;
        }
    }
    private void ExtraFollowTime()
    {
        if (targetLocked && canFollow == true)
        {
            followT += Time.deltaTime;
            if (followT > extraFollowTime) canFollow = false;
        }
    }
    private void RotateToTarget()
    {
        if (canFollow)
        {
            
            Quaternion direction = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotationSpeed * Time.deltaTime);
        }       
    }
    private void LockTarget()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward,out hit, 100))
        {
            if (hit.collider.gameObject.GetComponent<PlayerStatus>() != null)
                targetLocked = true;
        }
    }
    private void AlignToPlayer()
    {
        var lookPos = player.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        Vector3 direction;
        if (left) direction = new Vector3(-30, -45, 0);
        else direction = new Vector3(-30, 45, 0);

        transform.localEulerAngles += direction;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")||other.CompareTag("PlayerWeapon"))
        {
            PlayerSingleton.Instance.playerCurrentHP -= damage;
            PlayerSingleton.Instance.playerCurrentStamina -= DefaultPlayerVars.defaultMaxStamina*staminaDamage;
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        col.enabled = true;
        //Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward*100, Color.red);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeganeuraLaser : MonoBehaviour
{
    private GameObject player;
    private LineRenderer lr;
    public float rotationSpeed;
    public float damage;

    public bool isActive;
    public bool canRotate;
    public bool playerLockedOn;
    public bool damagePerTick;
    public bool damageDealt;
    private void Awake()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;
        lr = GetComponent<LineRenderer>();
    }
    private void OnEnable()
    {
        lr.startWidth = .05f;
        lr.endWidth = .05f;
        transform.LookAt(player.transform.position);
        isActive = false;
        canRotate = true;
        damageDealt = false;
    }
    private void Update()
    {
        if(canRotate)RotateToTarget();
        SetLineRendererPosition();

        if(isActive) DamagePlayer();

    }
    private void DamagePlayer()
    {
        lr.startWidth=1.2f;
        lr.endWidth=1.2f;
        
        if (playerLockedOn)
        {
            if (damagePerTick)
            {
                PlayerStatus.damagePlayer?.Invoke(damage * Time.deltaTime);
            }
            else if (damagePerTick == false && damageDealt == false)
            {
                PlayerStatus.damagePlayer?.Invoke(damage);
                damageDealt = true;
            }
        }
        
    }
    private void SetLineRendererPosition()
    {
        RaycastHit hit;
        lr.SetPosition(0, transform.position);
        if(Physics.Raycast(transform.position,transform.forward,out hit, 100))
        {
            lr.SetPosition(1, hit.point);
            if (hit.collider.gameObject==player)
            {
                playerLockedOn = true;
            }
            else
            {
                playerLockedOn = false;
            }
        }
        else
        {
            playerLockedOn = false;
            lr.SetPosition(1, transform.position+transform.forward*100);
        }
    }
    private void RotateToTarget()
    {
        Quaternion direction = Quaternion.LookRotation((player.transform.position+player.transform.up) - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotationSpeed * Time.deltaTime);       
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.forward * 100,Color.red);
    }
}
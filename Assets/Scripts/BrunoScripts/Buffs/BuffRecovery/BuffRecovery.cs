using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffRecovery : MonoBehaviour
{
    private GameObject player;
    public GeneralDataHolder holder;

    public Vector3 safeSpot;
    [SerializeField] private float distance;
    [SerializeField] private GameObject recoveryObject;
    [SerializeField] private GameObject existingRecoveryObject;
    private void OnEnable()
    {
        if(holder.spawnDataRecoveryObject) SpawnRecoveryObject();
        
    }
    
    void Start()
    {
        player = FindObjectOfType<PlayerStatus>().gameObject;

        try
        {
            existingRecoveryObject = FindObjectOfType<RecoveryObject>().gameObject;
        }
        catch
        {
            existingRecoveryObject = null;
        }
        
    }
    void Update()
    {
        if(existingRecoveryObject==null) DetectSafeGround();
        //if(Input.GetMouseButton(1)) PlayerSingleton.Instance.playerCurrentHP -= 10;
    }
    private void DetectSafeGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, -transform.up, out hit, distance))
        {
            if (hit.collider.gameObject.tag=="Ground")
            {
                safeSpot = hit.point;
                holder.lastSafeSpot = safeSpot;
            }
        }
    }
    public void SpawnRecoveryObject()
    {
               
        GameObject obj = Instantiate(recoveryObject);
        obj.transform.position = holder.lastSafeSpot;
        //holder.spawnDataRecoveryObject = false;
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(safeSpot, 1);
    }
}

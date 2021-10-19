using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform TargetObjectTF;
    [Range(1.0f, 15.0f)] public float TargetRadius;
    [Range(20.0f, 75.0f)] public float LaunchAngle;
    [Range(0.0f, 10.0f)] public float TargetHeightOffsetFromGround;
    public bool RandomizeHeightOffset;

    private Rigidbody rigid;

    [SerializeField]
    private float speedFactor;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Launch();
    }
    public void Launch()
    {
        Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 targetXZPos = new Vector3(TargetObjectTF.position.x, 0.0f, TargetObjectTF.position.z);

        transform.LookAt(targetXZPos);
        float R = Vector3.Distance(projectileXZPos, targetXZPos);
        float G = Physics.gravity.y;
        float tanAlpha = Mathf.Tan(LaunchAngle * Mathf.Deg2Rad);
        float H = (TargetObjectTF.position.y) - transform.position.y;
        float Vz = Mathf.Sqrt(G * R * R / (2.0f * (H - R * tanAlpha)));
        float Vy = tanAlpha * Vz;

        Vector3 localVelocity = new Vector3(0f, Vy/speedFactor, Vz*speedFactor);
        Vector3 globalVelocity = transform.TransformDirection(localVelocity);
        

        rigid.velocity = globalVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //MonstersSpawnerControl monsters = GameObject.Find("Manager").GetComponent<MonstersSpawnerControl>();
        


    }



}

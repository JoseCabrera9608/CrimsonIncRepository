using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTest : MonoBehaviour
{
    public float radius;
    public float explotionRadius;
    [Range(0,1)]
    public float t;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        float x = radius * Mathf.Cos(2 * Mathf.PI * t);
        float z = radius * Mathf.Sin(2 * Mathf.PI * t);
        Gizmos.DrawWireSphere(new Vector3(transform.position.x+ x, 0,transform.position.z+ z),explotionRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerTest : MonoBehaviour
{
    public Transform player;
    public Direction playerIsCurrentlyOn;

    public float angleBetween;
    [HideInInspector] public Vector3 direction;

    public GameObject[] pos;
    private void OnDrawGizmos()
    {
        direction = player.position - transform.position;
        angleBetween = Vector3.Angle(transform.forward, direction);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 100);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);

        Gizmos.DrawRay(pos[0].transform.position, pos[0].transform.forward*100);
        Gizmos.DrawRay(pos[1].transform.position, pos[1].transform.forward*100);


        
        if (direction.x < 0) playerIsCurrentlyOn = Direction.left;
        else playerIsCurrentlyOn = Direction.right;
    }
}
public enum Direction
{
    left,
    right
}

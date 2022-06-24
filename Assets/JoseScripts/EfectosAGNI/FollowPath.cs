using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public GameObject[] wayPoints;
    int current = 0;
    public float speed;
    float WPradius = 1;

   
    void Update()
    {
        if (Vector3.Distance(wayPoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current >= wayPoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[current].transform.position, Time.deltaTime * speed);

    }

 
   
    }

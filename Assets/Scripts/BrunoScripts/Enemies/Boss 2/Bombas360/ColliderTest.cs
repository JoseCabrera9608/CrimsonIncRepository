using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    public bool collided;

    private void OnTriggerEnter(Collider other)
    {
        if (other)
        {
            collided = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other)
        {
            collided = false;
        }
    }
}

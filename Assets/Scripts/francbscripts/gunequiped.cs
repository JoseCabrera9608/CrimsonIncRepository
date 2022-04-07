using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunequiped : MonoBehaviour
{
    public GameObject gun_hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "gun" && Input.GetKey(KeyCode.E))
        {
            other.transform.SetParent(gun_hand.transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            other.gameObject.tag = null;
        }
    }
}

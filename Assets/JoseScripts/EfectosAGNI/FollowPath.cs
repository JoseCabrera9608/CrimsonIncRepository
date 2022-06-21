using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public float speed;
    public bool changeTarget;

    void Start()
    {
       // gameObject.transform.LookAt(point1.transform);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(point1.transform.position* Time.deltaTime);
        /*
                if (transform.position == point1.transform.position)
                {

                    Debug.Log("LlegoALA POSICION");
                    changeTarget = true;
                }

                if (transform.position == point2.transform.position)
                {
                    changeTarget = false;
                }*/

                switch (changeTarget)
                {
                    case false:
                        gameObject.transform.LookAt(point1.transform);
                        transform.Translate(0f, 0f, speed * Time.deltaTime);
                        break;

                    case true:
                        gameObject.transform.LookAt(point2.transform);
                        transform.Translate(0f, 0f, speed * Time.deltaTime);
                        break;

                }



    }

 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == point2)
        {
            
            /* gameObject.transform.LookAt(point2.transform);
             transform.Translate(0f, 0f, speed * Time.deltaTime);*/
            changeTarget = false;
        }

        if (collision.gameObject == point1)
        {
            
            /*gameObject.transform.LookAt(point1.transform);
            transform.Translate(0f, 0f, speed * Time.deltaTime);*/
            changeTarget = true;
        }
    }
}

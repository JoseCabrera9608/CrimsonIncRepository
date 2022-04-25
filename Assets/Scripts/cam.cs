using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class cam : MonoBehaviour
{
    public GameObject[] pos;
    private int pos_ = 0;
    public int ff;
    public GameObject cam_;
    public GameObject tex;

    // Use this for initialization
    void Start()
    {
        cam_.transform.position = pos[pos_].transform.position;
        cam_.transform.rotation = pos[pos_].transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //If the c button is pressed, switch to the next camera
        //Set the camera at the current index to inactive, and set the next one in the array to active
       
    }

    public void switch_cam(int dir)
    {
        if (dir < 0)
        {   //f(currentCameraIndex==
            pos_ += 1;
            
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (pos_ <= pos.Length-1)
            {
                //  cam.gameObject.transform.position = pos[pos_ - 1].gameObject;
                cam_.gameObject.transform.position = pos[pos_].transform.position;
                cam_.gameObject.transform.rotation = pos[pos_].transform.rotation;
                //ebug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
            else
            {
                //pos[pos_ - 1].gameObject.SetActive(false);
                pos_ = 0;
                cam_.gameObject.transform.position = pos[pos_].transform.position;

                cam_.gameObject.transform.rotation = pos[pos_].transform.rotation;
                //ebug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
        }
        else
        {
            pos_ -= 1;
            ff = pos_;
            Debug.Log("C button has been pressed. Switching to the next camera");
            if (pos_ >= 0)
            {
                //  cam.gameObject.transform.position = pos[pos_ - 1].gameObject;
                cam_.gameObject.transform.position = pos[pos_].transform.position;
                cam_.gameObject.transform.rotation = pos[pos_].transform.rotation;
                //ebug.Log("Camera with name: " + cameras[currentCameraIndex].GetComponent<Camera>().name + ", is now enabled");
            }
            else
            {
                //pos[pos_ - 1].gameObject.SetActive(false);
                pos_ = pos.Length-1;
                cam_.gameObject.transform.position = pos[pos_].transform.position;

                cam_.gameObject.transform.rotation = pos[pos_].transform.rotation;

            }
        }
        ff = pos_;
        switch (pos_)
        {
            case 0:
               // tex.GetComponent<Canvas>().GetComponent<TextEditor>().text = "dfgdfgdfgd";
             break;

            case 1:

             break;

            case 2:

             break;

            case 3:

             break;

            case 4:

             break;

            case 5:

             break;

            case 6:

             break;
        }
    }
}
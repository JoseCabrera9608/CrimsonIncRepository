using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShowroomManager : MonoBehaviour
{

    public CinemachineVirtualCamera vcam;
    public CinemachineFreeLook defaultcam;
    public Vector3 room1cam;
    public Vector3 room2cam;
    public Vector3 room3cam;
    public Vector3 room4cam;
    public Vector3 room5cam;

    public GameObject Player;
    public MovementShowroom playerpos;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {

        playerpos = Player.GetComponent<MovementShowroom>();
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= 2)
        {
            defaultcam.Priority = 100;
            vcam.Priority = 10;

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerpos.transform.position = room1cam;
            vcam.transform.position = room1cam;
            timer = 0;
            vcam.Priority = 200;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            vcam.transform.position = room2cam;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            vcam.transform.position = room3cam;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            vcam.transform.position = room4cam;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            vcam.transform.position = room5cam;
        }
    }
}

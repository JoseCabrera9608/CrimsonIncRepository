using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerDebug : MonoBehaviour
{
    // Start is called before the first frame update

    public string input;
    public float prueba;
    public float speed;
    public float jump;
    public float attackdmg;
    public float dashspeed;
    public float dashlenght;
    public InputField speedInput;
    public InputField jumpInput;
    public InputField attackdmgInput;
    public InputField dashspeedInput;
    public InputField dashlenghtInput;
    public GameObject Player;
    public Movement player;
    public PlayerAttack playerAttack;

    public CinemachineCameraOffset cineoff;
    public CinemachineFreeLook cinefree;

    private void Awake()
    {
        player = Player.GetComponent<Movement>();
        playerAttack = Player.GetComponent<PlayerAttack>();

        speed = player.movSpeed;
        //jump = player.jumpForce;
        dashspeed = player.dashspeed;
        dashlenght = player.duraciondash;


        prueba = 11;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void PlayerAttack(string s)
    {

        input = s;
        playerAttack.damage = float.Parse(input);

    }

    public void PlayerSpeed(string s)
    {

        input = s;
        player.movSpeed = float.Parse(input);
        
    }

    public void PlayerJump(string s)
    {
        input = s;
        //player.jumpForce = float.Parse(input);

    }

    public void DashSpeed(string s)
    {
        input = s;
        player.dashspeed = float.Parse(input);

    }
    public void DashLenght(string s)
    {
        input = s;
        player.duraciondash = float.Parse(input);

    }

    public void DashCD(string s)
    {
        input = s;
        player.dashcd = float.Parse(input);

    }

    //CAMARA

    public void CameraXoffset(string s)
    {
        input = s;
        cineoff.m_Offset.x = float.Parse(input);

    }
    public void CameraYoffset(string s)
    {
        input = s;
        cineoff.m_Offset.y = float.Parse(input);

    }
    public void CameraZoffset(string s)
    {
        input = s;
        cineoff.m_Offset.z = float.Parse(input);

    }


    public void CameraXDamping(string s)
    {
        input = s;
        cinefree.GetRig(0).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XDamping = float.Parse(input);
        cinefree.GetRig(1).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XDamping = float.Parse(input);
        cinefree.GetRig(2).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_XDamping = float.Parse(input);

    }

    public void CameraYDamping(string s)
    {
        input = s;
        cinefree.GetRig(0).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_YDamping = float.Parse(input);
        cinefree.GetRig(1).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_YDamping = float.Parse(input);
        cinefree.GetRig(2).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_YDamping = float.Parse(input);

    }

    public void CameraZDamping(string s)
    {
        input = s;
        cinefree.GetRig(0).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_ZDamping = float.Parse(input);
        cinefree.GetRig(1).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_ZDamping = float.Parse(input);
        cinefree.GetRig(2).GetCinemachineComponent<CinemachineOrbitalTransposer>().m_ZDamping = float.Parse(input);

    }
}

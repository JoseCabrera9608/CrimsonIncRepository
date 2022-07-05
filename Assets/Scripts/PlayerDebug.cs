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
    public InputField WalkspeedInput;
    public InputField RunspeedInput;
    public InputField jumpInput;
    public InputField attackdmgInput;
    public InputField dashspeedInput;
    public InputField dashlenghtInput;
    public GameObject Player;
    public Movement player;
    public PlayerAttack playerAttack;

    public Sensibilidad sensi;
    public Slider sensislider;

    public float pruebaa;

    public Text sensitext;

    public Animator playeranim;

    public CinemachineCameraOffset cineoff;
    public CinemachineFreeLook cinefree;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        player = Player.GetComponent<Movement>();
        playerAttack = Player.GetComponent<PlayerAttack>();
        //sensi = GameObject.FindGameObjectWithTag("SensController").GetComponent<Sensibilidad>();

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
        
        if (sensitext != null)
        {
            sensitext.text = sensislider.value.ToString();

            if (sensislider.value <= 0)
            {
                sensislider.value = 0.1f;
            }
        }
        
        //sensitext.text = sensislider.value.ToString();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }



        //sensitext.text = sensislider.value.ToString("F2"); 
    }

    public void Sensi(string s)
    {

        input = s;
        //playerAttack.damage = float.Parse(input);
        sensislider.value = float.Parse(input);

    }

    public void PlayerAttack(string s)
    {

        input = s;
        //playerAttack.damage = float.Parse(input);
        PlayerSingleton.Instance.playerDamage = float.Parse(input);

    }

    public void Attack1Speed(string s)
    {

        input = s;
        playeranim.SetFloat("Attack1Speed", float.Parse(input));
    }
    public void Attack2Speed(string s)
    {

        input = s;
        playeranim.SetFloat("Attack2Speed", float.Parse(input));
    }
    public void Attack3Speed(string s)
    {

        input = s;
        playeranim.SetFloat("Attack3Speed", float.Parse(input));
    }

    public void WalkSpeed(string s)
    {

        input = s;
        player.walkSpeed = float.Parse(input);
        
    }
    public void RunSpeed(string s)
    {

        input = s;
        player.runSpeed = float.Parse(input);

    }

    public void AnimWalkSpeed(string s)
    {

        input = s;
        playeranim.SetFloat("WalkSpeed", float.Parse(input));
    }

    public void AnimRunSpeed(string s)
    {

        input = s;
        playeranim.SetFloat("RunSpeed", float.Parse(input));
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
        player.staminaMax = float.Parse(input);

    }

    //STAMINA
    public void StaminaAttack(string s)
    {

        input = s;
        playerAttack.staminaAttack = float.Parse(input);

    }
    public void StaminaRun(string s)
    {

        input = s;
        player.staminaRunValue = float.Parse(input);

    }
    public void StaminaDash(string s)
    {

        input = s;
        player.staminaDash = float.Parse(input);

    }
    public void StaminaRecovery(string s)
    {

        input = s;
        player.staminaRecovery = float.Parse(input);

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

    public void CameraRadius(string s)
    {
        input = s;
        cinefree.m_Orbits[0].m_Radius = float.Parse(input);
        cinefree.m_Orbits[1].m_Radius = float.Parse(input);
        cinefree.m_Orbits[2].m_Radius = float.Parse(input);

    }

    public void CameraFOV(string s)
    {
        input = s;
        cinefree.m_Lens.FieldOfView = float.Parse(input);

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

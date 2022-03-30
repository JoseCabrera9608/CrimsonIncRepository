using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        PlayerMovement player = Player.GetComponent<PlayerMovement>();

        speed = player.playerSpeed;
        jump = player.jumpForce;
        dashspeed = player.dashspeed;
        dashlenght = player.duraciondash;


        prueba = 11;
    }

    void Start()
    {
        PlayerMovement player = Player.GetComponent<PlayerMovement>();
        speedInput.text = player.playerSpeed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput (string s)
    {
        input = s;
        prueba = float.Parse(input);
        Debug.Log(input);
    }

    public void PlayerSpeed(string s)
    {
        PlayerMovement player = Player.GetComponent<PlayerMovement>();
        input = s;
        player.playerSpeed = float.Parse(input);
        
    }

    public void PlayerJump(string s)
    {
        PlayerMovement player = Player.GetComponent<PlayerMovement>();
        input = s;
        player.jumpForce = float.Parse(input);

    }

    public void DashSpeed(string s)
    {
        PlayerMovement player = Player.GetComponent<PlayerMovement>();
        input = s;
        player.dashspeed = float.Parse(input);

    }
    public void DashLenght(string s)
    {
        PlayerMovement player = Player.GetComponent<PlayerMovement>();
        input = s;
        player.duraciondash = float.Parse(input);

    }
}

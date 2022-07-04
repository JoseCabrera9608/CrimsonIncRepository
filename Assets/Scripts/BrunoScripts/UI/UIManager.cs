using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image dashFill;
    [SerializeField] private Image healing;

    [SerializeField] private Image staminaBarFill;

    private float prevHealthAmmount;
    private float prevHealingCharges;

    public float healthscale;
    public float healthlenght;

    public Image healthLimit;
    public Image staminaLimit;
    //=========TEMPORAL==========
    [SerializeField]private Movement movement;
    //=========TEMPORAL==========

    public bool gameIsPaused;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject sondaPanel;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        prevHealthAmmount = PlayerSingleton.Instance.playerCurrentHP;
        prevHealingCharges = PlayerSingleton.Instance.playerCurrentHealingCharges;
        movement = FindObjectOfType<Movement>();
        gameIsPaused = false;
        ResumeGame();
        CursorController("block");
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerHealth();
        UpdateDash();
        CheckPlayerHealing();

        PlayerInput();

    }

    #region HUD
    //======HEALTH=================
    private void CheckPlayerHealth()
    {
        if (PlayerSingleton.Instance.playerCurrentHP != prevHealthAmmount) UpdateHealthBarFill();
    }
  
    private void UpdateHealthBarFill()
    {

        healthscale = PlayerSingleton.Instance.playerMaxHP * 0.01f;
        healthlenght = (PlayerSingleton.Instance.playerMaxHP * 0.01f * 88) -726;

        healthLimit.transform.localPosition = new Vector3((PlayerSingleton.Instance.playerMaxHP * 0.01f * +300) - 300, 1.1f, 0);

        healthBarFill.fillAmount = PlayerSingleton.Instance.playerCurrentHP / PlayerSingleton.Instance.playerMaxHP;
        prevHealthAmmount = PlayerSingleton.Instance.playerCurrentHP;
        healthBarFill.transform.localScale = new Vector3(healthscale, 1, 1);
        healthBarFill.transform.localPosition = new Vector3(healthlenght, 393, 1);
    }

    private void CheckPlayerHealing()
    {
        if (PlayerSingleton.Instance.playerCurrentHealingCharges != prevHealingCharges) UpdateHealing();
    }

    private void UpdateHealing()
    {
        healing.fillAmount = PlayerSingleton.Instance.playerCurrentHealingCharges / PlayerSingleton.Instance.playerMaxHealingCharges;
        prevHealingCharges = PlayerSingleton.Instance.playerCurrentHealingCharges;
    }
    //======HEALTH=================





    //======DASH===================
    private void UpdateDash()
    {

        staminaLimit.transform.localPosition = new Vector3((PlayerSingleton.Instance.playerMaxStamina * 1/3 * +300) - 300, -27.1f, 0);

        dashFill.fillAmount = PlayerSingleton.Instance.playerCurrentStamina / PlayerSingleton.Instance.playerMaxStamina;
        dashFill.transform.localScale = new Vector3(PlayerSingleton.Instance.playerMaxStamina * 1/3, 1, 1);
        dashFill.transform.localPosition = new Vector3((PlayerSingleton.Instance.playerMaxStamina * 1/3 * 88) - 726, 443.7f, 1);
    }

    //======DASH===================
    #endregion

    #region GENERAL_UI
    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            if (!gameIsPaused) ResumeGame();
            if (gameIsPaused) PauseGame();
        }
    }
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        CursorController("unblock");
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        if(playerPanel!=null)playerPanel.SetActive(false);
        if(sondaPanel!=null)sondaPanel.SetActive(false);
        Time.timeScale = 1;
        CursorController("block"); 
        gameIsPaused = false;
    }
    public void CursorController(string action)
    {
        switch (action)
        {
            case "block":
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case "unblock":
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;

            default:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
    public void SimpleSceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenOptionsPanel()=> optionsPanel.SetActive(true);
    public void OpenPlayerPanel() => playerPanel.SetActive(true);
    public void OpenSondaPanel() => sondaPanel.SetActive(true);

    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
        //=======APLICAR AJUSTES============
    }

    public void ClosePlayerPanel()
    {
        playerPanel.SetActive(false);
        //=======APLICAR AJUSTES============
    }

    public void CloseSondaPanel()
    {
        sondaPanel.SetActive(false);
        //=======APLICAR AJUSTES============
    }
    #endregion
}



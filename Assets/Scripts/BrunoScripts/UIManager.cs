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
    //=========TEMPORAL==========
    [SerializeField]private Movement movement;
    //=========TEMPORAL==========

    public bool gameIsPaused;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
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
        CursorController("block");
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerHealth();
        UpdateDash();
        CheckPlayerHealing();

        PlayerInput();
        #region TEMPORAL
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerSingleton.Instance.playerCurrentHP -= 10;
            PlayerSingleton.Instance.playerCurrentHealingCharges -= 1;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerSingleton.Instance.playerCurrentHP += 10;
            PlayerSingleton.Instance.playerCurrentHealingCharges += 1;
        }
        #endregion
    }

    #region HUD
    //======HEALTH=================
    private void CheckPlayerHealth()
    {
        if (PlayerSingleton.Instance.playerCurrentHP != prevHealthAmmount) UpdateHealthBarFill();
    }
  
    private void UpdateHealthBarFill()
    {
        healthBarFill.fillAmount = PlayerSingleton.Instance.playerCurrentHP / PlayerSingleton.Instance.playerMaxHP;
        prevHealthAmmount = PlayerSingleton.Instance.playerCurrentHP;
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
    private void UpdateDash()=> dashFill.fillAmount = movement.dashingcd / movement.dashcd;

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
    public void CloseOptionsPanel()
    {
        optionsPanel.SetActive(false);
        //=======APLICAR AJUSTES============
    }
    #endregion
}



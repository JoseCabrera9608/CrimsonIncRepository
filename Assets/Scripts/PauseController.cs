using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject MenuPausa;
    GameObject MenuSettings;
    public bool pause;



    public float timer;

    public GameObject Video;

    //=========TEMPORAL==========
    [SerializeField] private Movement movement;
    //=========TEMPORAL==========

    public bool gameIsPaused;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject sondaPanel;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            if (!gameIsPaused) ResumeGame();
            if (gameIsPaused) PauseGame();
        }
        var videoPlayer = Video.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playbackSpeed = Time.timeScale;

        if (!videoPlayer.isPlaying && timer >3)
        {
            Debug.Log("SE ACABOO");
            timer = 0; 
        }
        //videoPlayer.loopPointReached += EndReached;
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
        if (playerPanel != null) playerPanel.SetActive(false);
        if (sondaPanel != null) sondaPanel.SetActive(false);
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
}

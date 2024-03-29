﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenuUi;

    public Scene GameScene;

    private void Start() {
        Pause();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        gamePaused = false;
    }
    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        gamePaused = true;
    }

    public void quitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();

    }
    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

}

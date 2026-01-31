using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static int currentLevel = 1;
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    private bool EscPressed = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public static void LoadNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene("Level "+currentLevel);
    }


    private void Update()
    {
        EscPressed = Input.GetKeyDown(KeyCode.Escape);
        if (EscPressed && !isPaused)
        {
            isPaused = true;
            PauseLevel();
        }
        else if (EscPressed && isPaused)
        {
            isPaused = false;
            ResumeLevel();
        }
    }

    private void ResumeLevel()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    private void PauseLevel()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level " + currentLevel);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}

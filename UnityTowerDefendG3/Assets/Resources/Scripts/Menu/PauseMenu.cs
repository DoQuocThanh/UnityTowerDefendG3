using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        Debug.Log("Hien thi");
        pauseMenu.SetActive(true);
        Time.timeScale = 0;

    }
    public void Home()
    {
        SceneManager.LoadScene(6);
        Time.timeScale = 1;

    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void Settings()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;


    }
    public void Resart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
}

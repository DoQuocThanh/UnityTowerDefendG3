using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene(6);
        Time.timeScale = 1;

    }
    public void Resume()
    {
       // pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public  class WinScript : MonoBehaviour
{
    public GameObject loseScene;
    public GameObject winScene;
    public static WinScript instance;

    private void Awake()
    {
        instance = this;
    }
    //public void Home()
    //{
    //    SceneManager.LoadScene(6);
    //    Time.timeScale = 1;

    //}
    public void Resume()
    {
       // pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void NextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Lấy tên của scene
        string sceneName = currentScene.name;
        Debug.Log("Tên scene hiện tại là: " + sceneName);
        // 01. Level 01
        string levelIndex = sceneName.PadLeft(2, '0');
        Debug.Log(levelIndex);

        int level = int.Parse(levelIndex);
        level = level + 1;
        Debug.Log(level); 


        string levelSceneName = level + ". Level " + level;
        SceneManager.LoadScene(levelSceneName);


    }
    public void BackToHome()
    {
        Debug.Log("Homeeee: ");

        SceneManager.LoadScene("MenuScene");
    }
    public void Resart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
    public void SelectLevel()
    {
        Debug.Log("Homeeee: ");

        SceneManager.LoadScene("00. Level Selection");
    }

}

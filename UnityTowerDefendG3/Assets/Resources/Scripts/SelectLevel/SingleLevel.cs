using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
  
    public void ClickBackButton()
    {
        SceneManager.LoadScene("00. Level Selection");
    }

    public void ClickWinButton()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        string levelIndex = activeScene.Split(".")[0];
        PlayerPrefs.SetInt("Lv" + levelIndex, 1);
        ClickBackButton();
    }
}

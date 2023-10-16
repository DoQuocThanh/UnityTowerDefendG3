using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        AudioManeger.Instance.PlaySFX("btn_Click");
        StartCoroutine(LoadGameScene());
    }
    private void Start()
    {
    
      AudioManeger.Instance.PlayMusic("Theme");

    }

    private IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.3f);
     //  AudioManeger.Instance.PlayMusic("Theme");
        // Sau khi chờ, tải Scene mới
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        AudioManeger.Instance.PlaySFX("btn_Click");
        Application.Quit();
        Debug.Log("Exit game!");
    }



}

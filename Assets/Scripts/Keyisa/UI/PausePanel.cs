using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public void OpenPausePanel()
    {
        gameObject.SetActive(true);
    }
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void ResumetGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        Data.lifes = 3;
        Data.scores = 0;
        Time.timeScale = 1f;
        gameObject.SetActive(false);

        SceneManager.LoadScene("MainScene");
    }
}

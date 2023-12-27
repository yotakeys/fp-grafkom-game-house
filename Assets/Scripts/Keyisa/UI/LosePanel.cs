using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Data.lifes = 3;
        Data.scores = 0;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public void GoToSpaceShooter()
    {
        SceneManager.LoadScene("Keyisa");
    }

    public void GoToDinoRun()
    {
        SceneManager.LoadScene("Dewangga");
    }

    public void GoToChess()
    {
        SceneManager.LoadScene("Aryan");
    }

    public void GoToFallenEnemies()
    {
        SceneManager.LoadScene("Baihaqi");
    }

    public void GoToFlappyBird()
    {
        SceneManager.LoadScene("Tengku");
    }

    public void GoToQuiz()
    {
        SceneManager.LoadScene("Ferza");
    }

    public void GoToJunez()
    {
        SceneManager.LoadScene("Junez");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFlappyBird : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}

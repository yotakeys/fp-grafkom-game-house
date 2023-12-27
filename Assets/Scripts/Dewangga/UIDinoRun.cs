using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDinoRun : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainScene");
    }
}

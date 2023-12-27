using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text announcement;

    public void SetText(string text)
    {
        announcement.text = text;
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainScene");
    }
}

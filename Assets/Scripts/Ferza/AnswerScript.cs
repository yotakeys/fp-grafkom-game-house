using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public Color startColor;
    public Color originalColor;

    private void Start()
    {
        startColor = GetComponent<Image>().color;
        originalColor = startColor;
    }

    public void Answer()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            UnityEngine.Debug.Log("Correct Answer");
            quizManager.correct();
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            UnityEngine.Debug.Log("Wrong Answer");
            quizManager.wrong();
        }
    }
}

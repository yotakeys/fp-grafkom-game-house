using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public QuizManager quizManager;

    public GameObject QuizPanel;
    public GameObject Gopanel;

    public Text QuextionTxt;
    public Text ScoreTxt;

    int totalQuestions = 0;
    public int score;

    private void Start()
    {
        totalQuestions = QnA.Count;
        Gopanel.SetActive(false);
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void backMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    void GameOver()
    {
        QuizPanel.SetActive(false);
        Gopanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(ShowNextQuestionAfterDelay(2f));
    }

    public void wrong()
    {
        //when you answer wrong
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(ShowNextQuestionAfterDelay(2f));
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            ResetColorsOfOptions();
            currentQuestion = Random.Range(0, QnA.Count);

            QuextionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            UnityEngine.Debug.Log("Out of Question");
            GameOver();
        }

    }

    void ResetColorsOfOptions()
    {
        foreach (GameObject option in options)
        {
            option.GetComponent<Image>().color = option.GetComponent<AnswerScript>().originalColor; // Menggunakan warna asli yang disimpan
        }
    }

    IEnumerator ShowNextQuestionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        generateQuestion(); // Memanggil generateQuestion setelah jeda waktu
    }

}

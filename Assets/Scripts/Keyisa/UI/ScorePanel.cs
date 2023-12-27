using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public Text score;
    public void Update()
    {
        score.text = "SCORE : " + Data.scores.ToString();
    }
}

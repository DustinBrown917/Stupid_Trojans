using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour {

    [SerializeField]
    private Text highScoreName;
    [SerializeField]
    private Text highScoreNumber;


    public void SetHighScore(HighScore highScore)
    {
        if(highScore == null)
        {
            highScoreName.text = "--";
            highScoreNumber.text = "--";
        }
        else
        {
            highScoreName.text = highScore.name;
            highScoreNumber.text = highScore.score.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    [SerializeField]
    private Text scoreLabel;

	// Use this for initialization
	void Start () {
        PlayerControl.Instance.ScoreChanged += PlayerControl_ScoreChanged;
        UpdateScoreLabel(0);
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayerControl_ScoreChanged(object sender, PlayerControl.ScoreChangedArgs e)
    {
        UpdateScoreLabel(e.newScore);
    }

    private void UpdateScoreLabel(int score)
    {
        scoreLabel.text = "X " + score.ToString();
    }


}

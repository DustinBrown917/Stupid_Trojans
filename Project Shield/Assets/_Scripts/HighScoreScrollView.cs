using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreScrollView : MonoBehaviour {

    [SerializeField]
    private GameObject highScoreDisplayObject;
    [SerializeField]
    private Transform content;

    private List<HighScoreDisplay> highScoreDisplays = new List<HighScoreDisplay>();

	// Use this for initialization
	void Start () {
		for(int i = 0; i < HighScoreManager.MaxNumOfHighScores; i++)
        {
            GameObject go = Instantiate(highScoreDisplayObject, content);
            highScoreDisplays.Add(go.GetComponent<HighScoreDisplay>());
        }

        HighScoreManager.NewHighScore += HighScoreManager_NewHighScore;

        UpdateScores();
	}

    private void HighScoreManager_NewHighScore(object sender, HighScoreManager.NewHighScoreArgs e)
    {
        UpdateScores();
    }

    private void UpdateScores()
    {
        
        for(int i = 0; i < highScoreDisplays.Count; i++)
        {
            highScoreDisplays[i].SetHighScore(HighScoreManager.GetHighScoreAt(i));
        }
    }

    public void DeleteHighScores()
    {
        HighScoreManager.ClearHighScores();
        UpdateScores();
    }
}

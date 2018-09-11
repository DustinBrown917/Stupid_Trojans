using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class HighScoreManager {

    public static int NumOfHighScores { get { return highScores.Count; } }
    private static List<HighScore> highScores = new List<HighScore>();
    private static int _maxNumOfHighScores = 10;
    public static int MaxNumOfHighScores { get { return _maxNumOfHighScores; } }

    private static string saveKey = "highScoresJson";


    static HighScoreManager()
    {
        LoadHighScores();
    }

    public static void AddHighScore( HighScore highScore )
    {
        if(highScores.Count == 0)
        {
            highScores.Insert(0, highScore);
            NewHighScoreArgs args = new NewHighScoreArgs() { newScore = highScore.score };
            OnNewHighScore(args);
        }
        else
        {
            Debug.Log("Got this far");
            for (int i = 0; i < highScores.Count; i++)
            {
                if (highScore.score > highScores[i].score)
                {
                    highScores.Insert(i, highScore);
                    if (highScores.Count > _maxNumOfHighScores) //cull high scores that exist beyond the limit.
                    {
                        for (int c = highScores.Count - 1; c >= _maxNumOfHighScores; c--)
                        {
                            highScores.RemoveAt(c);
                        }
                    }
                    NewHighScoreArgs args = new NewHighScoreArgs() { newScore = highScore.score };
                    OnNewHighScore(args);
                    return;
                }
            }
            if(highScores.Count < _maxNumOfHighScores)
            {
                highScores.Add(highScore);
                NewHighScoreArgs args = new NewHighScoreArgs() { newScore = highScore.score };
                OnNewHighScore(args);
            }
        }
    }

    public static HighScore GetLowestHighScore()
    {
        if(highScores.Count == 0)
        {
            return new HighScore("", 0);
        }
        else
        {
            return highScores[highScores.Count - 1];
        }       
    }

    public static HighScore GetHighScoreAt(int index)
    {
        if(index < 0 || index >= highScores.Count)
        {
            return null;
        }

        return highScores[index];
    }

    public static void SaveHighScores()
    {
        HighScoreWrapper wrapper = new HighScoreWrapper();
        wrapper.highScores = highScores.ToArray();
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(saveKey, json);
    }

    public static void LoadHighScores()
    {
        try
        {
            string json = "";

            if (PlayerPrefs.HasKey(saveKey))
            {
                json = PlayerPrefs.GetString(saveKey);
            }

            if (json != "{}")
            {
                HighScoreWrapper wrapper = JsonUtility.FromJson<HighScoreWrapper>(json);
                highScores = new List<HighScore>(wrapper.highScores);
            }
            else
            {
                highScores = new List<HighScore>();
            }
        } catch(Exception e)
        {
            BugLog.Instance.ShowException(e);
        }

    }

    public static void ClearHighScores()
    {
        PlayerPrefs.DeleteKey(saveKey);
        highScores = new List<HighScore>();
    }





    #region NewHighScore Event
    public static event EventHandler<NewHighScoreArgs> NewHighScore;

    public class NewHighScoreArgs : EventArgs
    {
        public int newScore;
    }

    private static void OnNewHighScore(NewHighScoreArgs args)
    {
        SaveHighScores();

        EventHandler<NewHighScoreArgs> handler = NewHighScore;

        if (handler != null)
        {
            handler(typeof(HighScoreManager), args);
        }
    }
    #endregion
}

[Serializable]
public class HighScore
{
    public int score;
    public string name;


    public HighScore(string name, int score)
    {
        this.score = score;
        this.name = name;
    }
}

[Serializable]
public class HighScoreWrapper
{
    public HighScore[] highScores;
}

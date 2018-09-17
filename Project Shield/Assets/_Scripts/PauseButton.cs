using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private Sprite playGraphic;
    [SerializeField]
    private Sprite pauseGraphic;

    // Use this for initialization
    void Start () {
        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
        GameManager.Instance.PauseStateChanged += GameManager_PauseStateChanged;
	}

    private void GameManager_PauseStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.Paused)
        {
            image.sprite = playGraphic;
        } else
        {
            image.sprite = pauseGraphic;
        }
    }

    private void GameManager_GameStateChanged(object sender, GameManager.GameStateChangedArgs e)
    {
        if(e.newState == GameManager.GameState.START_SCREEN || e.newState == GameManager.GameState.GAMEOVER)
        {
            SetButtonActive(false);
        } else if(e.newState == GameManager.GameState.PLAYING)
        {
            SetButtonActive(true);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void TogglePauseGame()
    {
        if (GameManager.Instance.Paused)
        {
            GameManager.Instance.UnpauseGame();            
        } else
        {
            GameManager.Instance.PauseGame();
        }
    }

    public void SetButtonActive(bool active)
    {
        pauseButton.SetActive(active);
    }
}

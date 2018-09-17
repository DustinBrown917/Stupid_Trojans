using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.Instance.PauseStateChanged += GameManager_PauseStateChanged;
	}

    private void GameManager_PauseStateChanged(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.Paused)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ReturnToStart()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.START_SCREEN);
        gameObject.SetActive(false);
    }
}

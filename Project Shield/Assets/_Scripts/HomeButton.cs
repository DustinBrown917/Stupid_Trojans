using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour {

    [SerializeField]
    private GameObject homeMenu;
    [SerializeField]
    private GameObject homeButton;

    private void Start()
    {
        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
    }

    private void GameManager_GameStateChanged(object sender, GameManager.GameStateChangedArgs e)
    {
        if(e.newState == GameManager.GameState.PLAYING)
        {
            homeButton.SetActive(true);
        } else
        {
            homeButton.SetActive(false);
        }
    }

    public void ShowHomeMenu()
    {
        GameManager.Instance.PauseGame();
        homeMenu.SetActive(true);
    }
}

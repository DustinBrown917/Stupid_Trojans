using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

    [SerializeField]
    private Text bigText;
    [SerializeField]
    private Text littleText;

    private void OnEnable()
    {
        if(GameManager.Instance.state == GameManager.GameState.GAMEOVER)
        {
            bigText.text = "GAME OVER";
        } else if(GameManager.Instance.state == GameManager.GameState.START_SCREEN)
        {
            bigText.text = "Stupid Trojans";
        }
    }

    private IEnumerator clickDelay()
    {
        yield return new WaitForSeconds(1f);
        littleText.enabled = true;
    }


    public void StartGame()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.PLAYING);
        gameObject.SetActive(false);
    }
}

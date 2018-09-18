using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    [SerializeField]
    private GameOverScreen gameOverScreen;

    public float LevelStartTime { get; private set; }

    public GameState state { get; private set; }
    public bool Paused { get; private set; }

    public int Deaths { get { return _deaths; } }
    private int _deaths = 0;
    public int DeathsAllowed { get { return _deathsAllowed; } }
    [SerializeField]
    private int _deathsAllowed = 10;

    /********************************************************************************************/
    /************************************* UNITY BEHAVIOURS *************************************/
    /********************************************************************************************/

    private void Awake()
    {
        if(_instance != null && _instance != this) //Singleton pattern...
        {
            Destroy(_instance);
        }

        _instance = this;

        state = GameState.START_SCREEN;
    }

    /********************************************************************************************/
    /**************************************** BEHAVIOURS ****************************************/
    /********************************************************************************************/

    /// <summary>
    /// Change the GameState
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeGameState(GameState newState)
    {
        if(newState == state) { return; }

        try
        {
            GameStateChangedArgs args = new GameStateChangedArgs();
            args.previousState = state;
            state = newState;
            args.newState = newState;

            if (newState == GameState.PLAYING)
            {
                SetDeaths(0);
                LevelStartTime = Time.time;
            }

            OnGameStateChanged(args);
            if (newState == GameState.GAMEOVER || newState == GameState.START_SCREEN)
            {
                gameOverScreen.gameObject.SetActive(true);
            }
            UnpauseGame();         
        }
        catch(Exception e) //Forward bugs to the BugLog
        {
            BugLog.Instance.gameObject.SetActive(true);
            BugLog.Instance.ShowException(e);
            throw e;
        }

    }

    /// <summary>
    /// Increase _deaths by 1.
    /// </summary>
    public void AddDeath()
    {
        if(_deaths >= _deathsAllowed) { return; }

        DeathsChangedArgs args = new DeathsChangedArgs();
        args.oldDeaths = _deaths;
        _deaths++;
        args.newDeaths = _deaths;

        OnDeathsChanged(args);

        if(_deaths == _deathsAllowed)
        {
            ChangeGameState(GameState.GAMEOVER);
        }
    }

    /// <summary>
    /// Set _deaths to a specified value.
    /// </summary>
    /// <param name="deaths">The number to set _deaths to.</param>
    public void SetDeaths(int deaths)
    {
        DeathsChangedArgs args = new DeathsChangedArgs();
        args.oldDeaths = this._deaths;
        this._deaths = deaths;
        args.newDeaths = this._deaths;

        OnDeathsChanged(args);
    }

    /// <summary>
    /// Close the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void PauseGame()
    {
        if (Paused) { return; }
        Paused = true;
        Time.timeScale = 0;
        OnPauseStateChanged();
    }

    /// <summary>
    /// Unpause the game. C'mon, you could've figured that one out.
    /// </summary>
    public void UnpauseGame()
    {
        if(!Paused) { return; }
        Paused = false;
        Time.timeScale = 1;
        OnPauseStateChanged();
    }


    /********************************************************************************************/
    /****************************************** EVENTS ******************************************/
    /********************************************************************************************/

    #region GameStateChanged Event.
    public event EventHandler<GameStateChangedArgs> GameStateChanged;

    public class GameStateChangedArgs : EventArgs
    {
        public GameState previousState;
        public GameState newState;
    }

    public void OnGameStateChanged(GameStateChangedArgs args)
    {
        EventHandler<GameStateChangedArgs> handler = GameStateChanged;

        if(handler != null)
        {
            handler(this, args);
        }
    }
    #endregion


    #region DeathsChanged Event.
    public event EventHandler<DeathsChangedArgs> DeathsChanged;

    public class DeathsChangedArgs : EventArgs
    {
        public int oldDeaths;
        public int newDeaths;
    }

    private void OnDeathsChanged(DeathsChangedArgs args)
    {
        EventHandler<DeathsChangedArgs> handler = DeathsChanged;

        if(handler != null)
        {
            handler(this, args);
        }
    }

    #endregion


    #region PauseStateChanged Event.
    public event EventHandler PauseStateChanged;

    private void OnPauseStateChanged()
    {
        EventHandler handler = PauseStateChanged;

        if(handler != null)
        {
            handler(this, EventArgs.Empty);
        }
    }

    #endregion


    /********************************************************************************************/
    /****************************************** ENUMS *******************************************/
    /********************************************************************************************/


    public enum GameState
    {
        START_SCREEN,
        PLAYING,
        GAMEOVER
    }
}

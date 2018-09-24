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

<<<<<<< HEAD
=======

>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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
<<<<<<< HEAD
    /// Change the GameState
    /// </summary>
    /// <param name="newState"></param>
=======
    /// Changes the GameState of the game.
    /// </summary>
    /// <param name="newState">The GameState to change to.</param>
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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
<<<<<<< HEAD
        catch(Exception e) //Forward bugs to the BugLog
=======
        catch(Exception e) // Push any errors to the BugLog.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
        {
            BugLog.Instance.gameObject.SetActive(true);
            BugLog.Instance.ShowException(e);
            throw e;
        }

    }

    /// <summary>
<<<<<<< HEAD
    /// Increase _deaths by 1.
=======
    /// Add a death to the _deaths score.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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
<<<<<<< HEAD
    /// Set _deaths to a specified value.
    /// </summary>
    /// <param name="deaths">The number to set _deaths to.</param>
=======
    /// Set the death score to a specified number.
    /// </summary>
    /// <param name="deaths">The number to set deaths to.</param>
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    public void SetDeaths(int deaths)
    {
        DeathsChangedArgs args = new DeathsChangedArgs();
        args.oldDeaths = this._deaths;
        this._deaths = deaths;
        args.newDeaths = this._deaths;

        OnDeathsChanged(args);
    }

    /// <summary>
<<<<<<< HEAD
    /// Close the application.
=======
    /// Quit the application.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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
<<<<<<< HEAD
    /// Unpause the game. C'mon, you could've figured that one out.
=======
    /// Unpause the game. C'mon, you could have figured that one out on your own.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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

<<<<<<< HEAD
    #region GameStateChanged Event.
=======
    /// <summary>
    /// Called when the GameState of the game changes.
    /// </summary>
    #region GameStateChanged Event
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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

<<<<<<< HEAD

    #region DeathsChanged Event.
=======
    /// <summary>
    /// Called when the death score changes.
    /// </summary>
    #region DeathsChanges Event.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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

<<<<<<< HEAD

=======
    /// <summary>
    /// Called when the game switches between paused and unpaused.
    /// </summary>
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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

<<<<<<< HEAD

=======
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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

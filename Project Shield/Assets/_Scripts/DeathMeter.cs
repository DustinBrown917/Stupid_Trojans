using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMeter : MonoBehaviour {

    [SerializeField]
    private GameObject deathSymbolPrefab;

    private Rigidbody2D[] deathSymbolRigidBodies;

    [SerializeField]
    private float symbolOffset = 0.5f;
    [SerializeField]
    private float variableTorque = 180;
    [SerializeField]
    private Vector2 popForce;
    [SerializeField]
    private float deactivateSymbolTime = 5;
    private WaitForSeconds wfs_deactivate;

    private int deathSymbolIndex;

    /********************************************************************************************/
    /************************************* UNITY BEHAVIOURS *************************************/
    /********************************************************************************************/

<<<<<<< HEAD

    /********************************************************************************************/
    /************************************* UNITY BEHAVIOURS *************************************/
    /********************************************************************************************/

=======
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    void Start () {
        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
        GameManager.Instance.DeathsChanged += GameManager_DeathsChanged;
        InitializeDeathSymbols();
        wfs_deactivate = new WaitForSeconds(deactivateSymbolTime);
	}

<<<<<<< HEAD

    /********************************************************************************************/
    /************************************* EVENT RESPONDERS *************************************/
=======
    /********************************************************************************************/
    /************************************* EVENT LISTENERS **************************************/
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /********************************************************************************************/

    private void GameManager_DeathsChanged(object sender, GameManager.DeathsChangedArgs e)
    {
        Debug.Log(e.newDeaths + " vs " + e.oldDeaths);
        if (e.newDeaths > e.oldDeaths)
        {
            PopDeath();
        }
    }

    private void GameManager_GameStateChanged(object sender, GameManager.GameStateChangedArgs e)
    {
        if (e.newState == GameManager.GameState.PLAYING)
        {
            if (deathSymbolRigidBodies.Length != GameManager.Instance.DeathsAllowed)
            {
                InitializeDeathSymbols();
            }
            ResetDeathSymbols();
        }
    }

    /********************************************************************************************/
    /**************************************** BEHAVIOURS ****************************************/
    /********************************************************************************************/

    /// <summary>
<<<<<<< HEAD
    /// Create appropriate number of death symbols.
=======
    /// Create death symbols.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    private void InitializeDeathSymbols()
    {
        deathSymbolRigidBodies = new Rigidbody2D[GameManager.Instance.DeathsAllowed];
        for (int i = 0; i < deathSymbolRigidBodies.Length; i++)
        {
            deathSymbolRigidBodies[i] = Instantiate(deathSymbolPrefab, transform).GetComponent<Rigidbody2D>();
            deathSymbolRigidBodies[i].isKinematic = true;
        }
    }

    /// <summary>
<<<<<<< HEAD
    /// Layout death symbols at bottom of screen and ensure they are free of physics simulation.
=======
    /// Remove death symbols from physics simulation and lay them out at bottom of screen.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    private void ResetDeathSymbols()
    {
        StopAllCoroutines();
        Vector2 pos = new Vector2();
        deathSymbolIndex = deathSymbolRigidBodies.Length - 1;
        for (int i = 0; i < deathSymbolRigidBodies.Length; i++)
        {
            pos.x = (i + 1) * symbolOffset;
            deathSymbolRigidBodies[i].transform.localPosition = pos;
            deathSymbolRigidBodies[i].isKinematic = true;
            deathSymbolRigidBodies[i].velocity = new Vector2();
            deathSymbolRigidBodies[i].angularVelocity = 0;
            deathSymbolRigidBodies[i].transform.rotation = new Quaternion();
        }
    }

    /// <summary>
<<<<<<< HEAD
    /// Add the last death symbol to the physics simulation and add upward force.
=======
    /// Add the trailing death symbol to the physics simulation and pop it upwards with a small vertical force.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    private void PopDeath()
    {
        Debug.Log("Popped");
        deathSymbolRigidBodies[deathSymbolIndex].isKinematic = false;
        deathSymbolRigidBodies[deathSymbolIndex].AddForce(popForce);
        deathSymbolRigidBodies[deathSymbolIndex].angularVelocity = UnityEngine.Random.Range(-variableTorque, variableTorque);
        StartCoroutine(DeactivateSymbolTimer(deathSymbolRigidBodies[deathSymbolIndex]));
        if(deathSymbolIndex > 0) { deathSymbolIndex--; }
    }
    
    /// <summary>
<<<<<<< HEAD
    /// Routine to remove death symbol from physics simulation after a delay.
    /// </summary>
    /// <param name="rb2d"></param>
=======
    /// Coroutine to remove a death symbol from the physics simulation after a delay.
    /// </summary>
    /// <param name="rb2d">The death symbol to remove from physics simulation.</param>
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// <returns></returns>
    private IEnumerator DeactivateSymbolTimer(Rigidbody2D rb2d)
    {
        yield return wfs_deactivate;
        rb2d.isKinematic = true;
        rb2d.velocity = new Vector2();
    }
}

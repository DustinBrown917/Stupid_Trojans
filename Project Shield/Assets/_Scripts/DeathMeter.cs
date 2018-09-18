﻿using System.Collections;
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

    void Start () {
        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
        GameManager.Instance.DeathsChanged += GameManager_DeathsChanged;
        InitializeDeathSymbols();
        wfs_deactivate = new WaitForSeconds(deactivateSymbolTime);
	}


    /********************************************************************************************/
    /************************************* EVENT RESPONDERS *************************************/
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
    /// Create appropriate number of death symbols.
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
    /// Layout death symbols at bottom of screen and ensure they are free of physics simulation.
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
    /// Add the last death symbol to the physics simulation and add upward force.
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
    /// Routine to remove death symbol from physics simulation after a delay.
    /// </summary>
    /// <param name="rb2d"></param>
    /// <returns></returns>
    private IEnumerator DeactivateSymbolTimer(Rigidbody2D rb2d)
    {
        yield return wfs_deactivate;
        rb2d.isKinematic = true;
        rb2d.velocity = new Vector2();
    }
}

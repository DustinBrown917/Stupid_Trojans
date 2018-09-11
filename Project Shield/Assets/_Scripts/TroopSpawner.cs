using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSpawner : MonoBehaviour {

    public GameObject troop;
    public float minWaitTime = 0.15f;
    public float maxWaitTime = 1f;
    [SerializeField]
    private float baseSpawnTime = 3f;
    [SerializeField, Tooltip("The number of seconds to wait before increasing the spawn frequency.")]
    private float difficultyTimeScale = 20f;
    [SerializeField, Tooltip("The amount of base spawn time taken off with each difficulty increment.")]
    private float difficultyIncrementScale = 0.1f;
    private Coroutine cr_SpawnCycle = null;

	// Use this for initialization
	void Start () {
        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
	}

    private void GameManager_GameStateChanged(object sender, GameManager.GameStateChangedArgs e)
    {
        if(e.newState == GameManager.GameState.PLAYING)
        {
            StartSpawning();
        } else 
        {
            StopSpawning();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartSpawning()
    {
        StopSpawning();
        cr_SpawnCycle = StartCoroutine(SpawnCycle());
    }

    public void StopSpawning()
    {
        if(cr_SpawnCycle != null)
        {
            StopCoroutine(cr_SpawnCycle);
            cr_SpawnCycle = null;
        }
    }

    private IEnumerator SpawnCycle()
    {
        while (true)
        {
            float modifiedBaseSpawnTime = Mathf.Clamp(baseSpawnTime - (((Time.time - GameManager.Instance.LevelStartTime)/difficultyTimeScale) * difficultyIncrementScale), 0f, 99f);
            DeployTroop();
            yield return new WaitForSeconds(UnityEngine.Random.Range(minWaitTime, maxWaitTime) + modifiedBaseSpawnTime);
        }
    }

    private Troop GetTroop()
    {
        Troop t;

        if(Troop.Troops.Count > 0)
        {
            t = Troop.Troops[0];
            Troop.Troops.RemoveAt(0);
        } else
        {
            t = Instantiate(troop, transform).GetComponent<Troop>();
        }

        return t;
    }

    private void DeployTroop()
    {
        Troop t = GetTroop();
        t.transform.position = transform.position;
        t.gameObject.SetActive(true);
        t.Resurrect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archers : MonoBehaviour {

    public static Archers Instance { get; set; }

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Vector2 maxLaunchForce = new Vector2(-1f, 1f);
    [SerializeField]
    private Vector2 minLaunchForce = new Vector2(-1f, 1f);
    [SerializeField]
    private int baseNumberOfArrows = 1;
    [SerializeField, Tooltip("Number of seconds to wait before adding another arrow to the volley.")]
    private float difficultyIncrement = 25;
    [SerializeField]
    private float timeBetweenLaunches = 3f;

    private Coroutine cr_LaunchSequence = null;

	// Use this for initialization
	void Start () {
        Instance = this;

        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
	}

    private void GameManager_GameStateChanged(object sender, GameManager.GameStateChangedArgs e)
    {
        if(GameManager.Instance.state != GameManager.GameState.PLAYING)
        {
            StopLaunching();
        } else 
        {
            StartLaunching();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void StartLaunching()
    {
        StopLaunching();
        cr_LaunchSequence = StartCoroutine(LaunchSequence());
    }

    public void StopLaunching()
    {
        if(cr_LaunchSequence != null)
        {
            StopCoroutine(cr_LaunchSequence);
            cr_LaunchSequence = null;
        }
    }

    private void LaunchProjectile()
    {
        int numOfArrows = (int)((Time.time - GameManager.Instance.LevelStartTime) / difficultyIncrement) + baseNumberOfArrows;
        int c = 0;
        while(c < numOfArrows)
        {
            Rigidbody2D rb2d = GetProjectile().GetComponent<Rigidbody2D>();
            Vector2 launchForce = new Vector2(UnityEngine.Random.Range(minLaunchForce.x, maxLaunchForce.x), UnityEngine.Random.Range(minLaunchForce.y, maxLaunchForce.y));
            rb2d.AddForce(launchForce);
            c++;
        }

    }

    private GameObject GetProjectile()
    {
        GameObject go;
        if(Arrow.Arrows.Count > 0)
        {
            go = Arrow.Arrows[0].gameObject;
            Arrow.Arrows.RemoveAt(0);
        } else
        {
            go = Instantiate(projectile);
        }

        go.transform.parent = transform;
        go.transform.position = this.transform.position;
        go.SetActive(true);
        Arrow a = go.GetComponent<Arrow>();
        a.Revert();
        a.EnableTrail();

        return go;
    }

    private IEnumerator LaunchSequence()
    {
        float t = timeBetweenLaunches;

        while(t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        LaunchProjectile();

        cr_LaunchSequence = StartCoroutine(LaunchSequence());
    }
}

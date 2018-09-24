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

    private AudioSource audioSource;
    private Coroutine cr_LaunchSequence = null;
    private Animator animator;
    private WaitForSeconds shotAnimationDelay = new WaitForSeconds(0.4f);


    /********************************************************************************************/
    /************************************* UNITY BEHAVIOURS *************************************/
    /********************************************************************************************/

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Start () {
        Instance = this; //Singleton pattern...

        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
	}

    /********************************************************************************************/
<<<<<<< HEAD
    /************************************* EVENT RESPONDERS *************************************/
    /********************************************************************************************/

    /// <summary>
    /// Respond to GameState changes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
=======
    /************************************* EVENT LISTENERS **************************************/
    /********************************************************************************************/

>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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

    /********************************************************************************************/
    /**************************************** BEHAVIOURS ****************************************/
    /********************************************************************************************/

    /// <summary>
<<<<<<< HEAD
    /// Begin the recursive launching pattern.
=======
    /// Start the repeating launch pattern.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    public void StartLaunching()
    {
        StopLaunching();
        cr_LaunchSequence = StartCoroutine(LaunchSequence());
    }

    /// <summary>
<<<<<<< HEAD
    /// Stop and dispose of launch sequence coroutines.
=======
    /// Stop the repeating lauch pattern.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    public void StopLaunching()
    {
        if(cr_LaunchSequence != null)
        {
            StopCoroutine(cr_LaunchSequence);
            cr_LaunchSequence = null;
        }
    }

    /// <summary>
    /// Launch projectiles.
    /// </summary>
    private void LaunchProjectile()
    {
        //Get num of projectiles to launch
        int numOfArrows = (int)((Time.time - GameManager.Instance.LevelStartTime) / difficultyIncrement) + baseNumberOfArrows;
        int c = 0;
        //Place each projectile and apply a random force.
        while (c < numOfArrows)
        {
            Rigidbody2D rb2d = GetProjectile().GetComponent<Rigidbody2D>();
            //Add random force to projectile.
            Vector2 launchForce = new Vector2(UnityEngine.Random.Range(minLaunchForce.x, maxLaunchForce.x), UnityEngine.Random.Range(minLaunchForce.y, maxLaunchForce.y));
            rb2d.AddForce(launchForce);
            c++;
        }
        audioSource.Play();
    }

    /// <summary>
<<<<<<< HEAD
    /// Retrieve a projectile.
    /// </summary>
    /// <returns>Projectile GameObject</returns>
    private GameObject GetProjectile()
    {
        GameObject go;
        
        if(Arrow.Arrows.Count > 0) //If there are pooled arrows, grab one of those.
        {
            go = Arrow.Arrows[0].gameObject;
            Arrow.Arrows.RemoveAt(0);
        } else //Otherwise, instantiate one.
=======
    /// Retrieve a projectile GameObject.
    /// </summary>
    /// <returns>A projectile GameObject.</returns>
    private GameObject GetProjectile()
    {
        GameObject go;
        if(Arrow.Arrows.Count > 0) //If there are Arrows in the pool, take from there...
        {
            go = Arrow.Arrows[0].gameObject;
            Arrow.Arrows.RemoveAt(0);
        } else //... otherwise instantiate a new one.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
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

    /// <summary>
<<<<<<< HEAD
    /// Recursive launch pattern that will launch projectiles at a set interval.
=======
    /// Recurseive coroutine that will continually launch projectiles until explicitly stopped.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchSequence()
    {
        float t = timeBetweenLaunches;

        while(t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
        }
        animator.Play("shoot");
        yield return shotAnimationDelay;
        LaunchProjectile();

        cr_LaunchSequence = StartCoroutine(LaunchSequence());
    }
}

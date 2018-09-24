using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Collider2D col2d;
    private bool isFlying = true;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip blockedSound;


    public static List<Arrow> Arrows = new List<Arrow>();

    /********************************************************************************************/
    /************************************* UNITY BEHAVIOURS *************************************/
    /********************************************************************************************/

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        col2d = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }
<<<<<<< HEAD

=======
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
	
	void Update () {
        if (isFlying)
        {
<<<<<<< HEAD
            transform.up = rb2d.velocity; //Point the arrow in the direction it is moving.
=======
            transform.up = rb2d.velocity; //Point arrow in direction of movement.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
        }
	}

    private void OnDisable()
    {
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10) //10 = player layer
        {
            audioSource.clip = blockedSound;
            audioSource.Play();
        }
        
<<<<<<< HEAD
        //Remove from physics calculations and set as harmless child of collided object.
=======
        //Remove Arrow from physics sim and add as child of collided object.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
        rb2d.isKinematic = true;
        col2d.enabled = false;
        isFlying = false;
        rb2d.velocity = new Vector2();
        rb2d.angularVelocity = 0;
        StartCoroutine(Fade(2f));
        transform.parent = collision.gameObject.transform;

        DisableTrail();
    }

    /********************************************************************************************/
    /**************************************** BEHAVIOURS ****************************************/
    /********************************************************************************************/

    /// <summary>
<<<<<<< HEAD
    /// Return the arrow to its initialized state.
=======
    /// Return the Arrow to its initialized state.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    public void Revert()
    {
        rb2d.isKinematic = false;
        col2d.enabled = true;
        isFlying = true;
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }

    /// <summary>
<<<<<<< HEAD
    /// Enable the trail renderer component.
=======
    /// Enable the Arrow's trail renderer component.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    public void EnableTrail()
    {
        trailRenderer.enabled = true;
        trailRenderer.Clear();
    }

    /// <summary>
<<<<<<< HEAD
    /// Disable the trail renderer component.
=======
    /// Disable the Arrow's trail renderer component.
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// </summary>
    public void DisableTrail()
    {
        trailRenderer.enabled = false;
    }

    /// <summary>
<<<<<<< HEAD
    /// Fade the arrow's alpha to 0 then disable.
    /// </summary>
    /// <param name="time"></param>
=======
    /// Fades the Arrow's alpha to 0 before disabling it.
    /// </summary>
    /// <param name="time">The length of time the fade should take.</param>
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
    /// <returns></returns>
    private IEnumerator Fade(float time)
    {
        float t = time;
        Color color = spriteRenderer.color;
        while(t > 0)
        {
            t -= Time.deltaTime;
            color.a = t / time;
            spriteRenderer.color = color;
            yield return null;
        }
        DisableTrail();
        Arrows.Add(this); //Pool the arrow.
        gameObject.SetActive(false);
    }
<<<<<<< HEAD



=======
>>>>>>> 11bcf5b6fe9f3722a181c2a023232ad44a381d09
}

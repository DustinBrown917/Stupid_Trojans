using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour {

    private Rigidbody2D rb2d;
    private bool isAlive = true;
    private SpriteRenderer spriteRenderer;

    public static List<Troop> Troops = new List<Troop>();

    private Coroutine fading = null;

    [SerializeField]
    private float speed = 3;

    private Vector2 speedVector = new Vector2();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        speedVector.x = speed;
        GameManager.Instance.GameStateChanged += GameManager_GameStateChanged;
	}

    private void GameManager_GameStateChanged(object sender, GameManager.GameStateChangedArgs e)
    {
        if(e.newState == GameManager.GameState.GAMEOVER)
        {
            Kill();
        }
    }

    // Update is called once per frame
    void Update () {
        if (isAlive)
        {
            rb2d.velocity = speedVector;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile" && fading == null)
        {
            Kill();

        } else if(collision.gameObject.tag == "Gate")
        {
            Succeed();
        }
    }

    private void Kill()
    {

        if (!isAlive) { return; }
        isAlive = false;
        rb2d.AddForce(new Vector2(-10f, 50f));
        rb2d.angularVelocity = -180f;

        if (gameObject.activeSelf) { fading = StartCoroutine(FadeThenDestroy(5)); }
        

        gameObject.layer = 13;

        GameManager.Instance.AddDeath();
    }

    private void Succeed()
    {
        PlayerControl.Instance.IncrementScore(1);
        Troops.Add(this);
        gameObject.SetActive(false);
    }

    private IEnumerator FadeThenDestroy(float time)
    {
        float t = time;
        Color color = spriteRenderer.color;
        while (t > 0)
        {
            t -= Time.deltaTime;
            color.a = t / time;
            spriteRenderer.color = color;
            yield return null;
        }
        Troops.Add(this);
        fading = null;
        gameObject.SetActive(false);
    }

    public void Resurrect()
    {
        isAlive = true;
        gameObject.layer = 11;
        Color col = spriteRenderer.color;
        col.a = 1;
        spriteRenderer.color = col;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

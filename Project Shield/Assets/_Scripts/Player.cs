using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float accelerationSpeed = 0.1f;
    private Rigidbody2D rb2d;

    private Vector2 translationVector = new Vector2();
    [SerializeField]
    private GameObject shieldUmbrella;
    [SerializeField]
    private GameObject shieldProjectile;
    [SerializeField]
    private Vector2 shieldThrowForce;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        
	}



    // Update is called once per frame
    void Update () {

        if(GameManager.Instance.state != GameManager.GameState.PLAYING) { return; }

        if (Input.GetKey(KeyCode.A))
        {
            if(!(translationVector.x <= -speed))
            {
                translationVector.x-= accelerationSpeed;
            }
            
            rb2d.velocity = translationVector;

        } else if (Input.GetKey(KeyCode.D))
        {
            if(!(translationVector.x >= speed))
            {
                translationVector.x += accelerationSpeed;
            }

            rb2d.velocity = translationVector;
        }

        if(Input.GetKeyDown(KeyCode.S) && !shieldUmbrella.activeSelf)
        {
            DeployShieldUmbrella();
        }

        if(Input.GetKeyDown(KeyCode.W) && !shieldProjectile.activeSelf)
        {
            DeployShieldProjectile();
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            translationVector = new Vector2();
        }

	}

    private void DeployShieldUmbrella()
    {
        Vector3 pos = new Vector3(transform.position.x, shieldUmbrella.transform.position.y, shieldUmbrella.transform.position.z);
        shieldUmbrella.transform.position = pos;
        shieldUmbrella.SetActive(true);
    }

    private void DeployShieldProjectile()
    {
        shieldProjectile.SetActive(true);
        shieldProjectile.transform.position = transform.position;
        Rigidbody2D shieldRb2d = shieldProjectile.GetComponent<Rigidbody2D>();
        shieldRb2d.AddForce(shieldThrowForce);
        shieldRb2d.AddTorque(UnityEngine.Random.Range(-30f, 30f));
    }
}

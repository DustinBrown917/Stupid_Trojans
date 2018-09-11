using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour {

    [SerializeField]
    private GameObject plusOneTextObject;
    [SerializeField]
    private Color textColor;
    [SerializeField]
    private float delayBeforeFade = 2;
    [SerializeField]
    private float fadeTime = 2;
    [SerializeField]
    private Vector2 maxScoreForce;
    [SerializeField]
    private Vector2 minScoreForce;
    [SerializeField]
    private float variableScoreTorque;
    

    private Queue<GameObject> plusOneTextPool = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
        PlayerControl.Instance.ScoreChanged += PlayerControl_ScoreChanged;
	}

    private void PlayerControl_ScoreChanged(object sender, PlayerControl.ScoreChangedArgs e)
    {
        if(e.newScore > e.oldScore)
        {
            CreatePlusOneText();
        }
        
    }

    private void CreatePlusOneText()
    {
        GameObject floatingText = GetPlusOneText();


        StartCoroutine(LaunchScoreThenFade(floatingText, fadeTime));
        
    }

    private GameObject GetPlusOneText()
    {
        GameObject go;
        if(plusOneTextPool.Count > 0)
        {
            go = plusOneTextPool.Dequeue();

        } else
        {
            go = Instantiate(plusOneTextObject);
        }

        ResetPlusOneText(go);

        return go;
    }

    private void ResetPlusOneText(GameObject go)
    {
        go.GetComponentInChildren<Text>().color = textColor;
        go.transform.position = transform.position;
        go.transform.rotation = new Quaternion();
        go.SetActive(true);
    }



    private IEnumerator LaunchScoreThenFade(GameObject go, float time)
    {

        float t = time;
        float factor;

        Text text = go.GetComponentInChildren<Text>();
        Color col = text.color;
        Rigidbody2D rb2d = go.GetComponent<Rigidbody2D>();

        Vector2 launchForce = new Vector2(UnityEngine.Random.Range(minScoreForce.x, maxScoreForce.x), UnityEngine.Random.Range(minScoreForce.y, maxScoreForce.y));

        rb2d.AddForce(launchForce);
        rb2d.AddTorque(UnityEngine.Random.Range(-variableScoreTorque, variableScoreTorque));

        yield return new WaitForSeconds(delayBeforeFade);

        while (t > 0)
        {
            t -= Time.deltaTime;
            factor = t / time;
            col.a = factor;
            text.color = col;
            yield return null;
        }

        go.SetActive(false);
        plusOneTextPool.Enqueue(go);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {

    [SerializeField]
    private float rotationSpeed;

    private Quaternion initialRotaion;

    private void Awake()
    {
        initialRotaion = transform.rotation;
    }

    private void OnEnable()
    {
        transform.rotation = initialRotaion;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, rotationSpeed);
	}
}

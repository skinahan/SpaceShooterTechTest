using UnityEngine;
using System.Collections;

public class MoveConvoyScript : MonoBehaviour {
    Rigidbody rb = null;

    public float convoyThrust = 900f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        rb.AddForce(transform.forward * convoyThrust, ForceMode.Acceleration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

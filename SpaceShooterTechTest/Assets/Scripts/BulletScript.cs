using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    Rigidbody rb = null;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 10f, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position += Time.deltaTime * speed * transform.up;
	}
}

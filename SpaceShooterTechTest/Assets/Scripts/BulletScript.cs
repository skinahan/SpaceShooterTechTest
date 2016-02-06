using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
    Rigidbody rb = null;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * 50f, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > 1000)
            Destroy(this.gameObject);
        if (transform.position.x < -1000)
            Destroy(this.gameObject);
        if (transform.position.y > 1000)
            Destroy(this.gameObject);
        if (transform.position.y < -1000)
            Destroy(this.gameObject);
        if (transform.position.z > 1000)
            Destroy(this.gameObject);
        if (transform.position.z < -1000)
            Destroy(this.gameObject);
        //transform.position += Time.deltaTime * speed * transform.up;
	}
}

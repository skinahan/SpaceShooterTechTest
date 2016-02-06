using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody rb = null;

    public float mainThrust = 400f;
    public float verticalThrust = 200f;
    public float strafeThrust = 200f;
    public float rotateThrust = 200f;
    public bool inertiaDamping = false;
    private float spawnDistance = 3f;
    public Transform bullet;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
	
    void FixedUpdate ()
    {
        if(Input.GetButtonDown("inertiaDamp"))
		{
            inertiaDamping = !inertiaDamping;
		}
        if(Input.GetButtonDown("Fire1"))
        {
            Quaternion F = transform.rotation;
            GameObject.Instantiate(bullet, transform.position + spawnDistance * transform.forward, F * Quaternion.Euler(90,0,0));
        }
    }

	// Update is called once per frame
	void Update () {

        if(inertiaDamping)
        {
            rb.drag = 0.5f;
            rb.angularDrag = 2f;
        } else
        {
            rb.drag = 0f;
            rb.angularDrag = 0.05f;
        }

        float h = Input.GetAxis("Horizontal") * strafeThrust * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * mainThrust * Time.deltaTime;

        float clockwise = Input.GetAxis("Clockwise") * rotateThrust * Time.deltaTime;

        float x = Input.GetAxis("Jump") * verticalThrust * Time.deltaTime;        

        rb.AddForce(transform.forward * v, ForceMode.Acceleration);
        rb.AddForce(transform.right * h, ForceMode.Acceleration);
        rb.AddForce(transform.up * x, ForceMode.Acceleration);

        rb.AddTorque(-transform.forward * clockwise);
    }
}

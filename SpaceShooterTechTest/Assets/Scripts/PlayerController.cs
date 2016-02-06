using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody rb = null;

    public float mainThrust = 400f;
    public float verticalThrust = 200f;
    public float strafeThrust = 200f;
    public float rotateThrust = 200f;
    public bool inertiaDamping = false;

    public Image forwardMovementImage;
    public Image backwardMovementImage;
    public Image leftMovementImage;
    public Image rightMovementImage;

    private Color startColor;

    private float spawnDistance = 3f;
    public Transform bullet;

    private bool movingForward = false;
    private bool movingRight = false;
    private bool movingLeft = false;
    private bool movingBackward = false;

    void Start()
    {
        startColor = forwardMovementImage.color;
        rb = GetComponent<Rigidbody>();
    }
	
    void FixedUpdate ()
    {
        float forwardVelocity = Vector3.Dot(rb.velocity, transform.forward);
        float rightwardVelocity = Vector3.Dot(rb.velocity, transform.right);
        Vector3 localangularvelocity = transform.InverseTransformDirection(rb.angularVelocity).normalized * rb.angularVelocity.magnitude;

        if (forwardVelocity == 0)
        {
            movingForward = false;
            movingBackward = false;
        }

        if(rightwardVelocity == 0)
        {
            movingRight = false;
            movingLeft = false;
        }

        if (forwardVelocity > 1)
        {
            movingForward = true;
        }
        else
        {
            movingForward = false;
        }

        if(forwardVelocity < -1)
        {
            movingBackward = true;
        }
        else
        {
            movingBackward = false;
        }

        if(rightwardVelocity > 1)
        {
            movingRight = true;
        }
        else
        {
            movingRight = false;
        }

        if (rightwardVelocity < -1)
        {            
            movingLeft = true;
        }
        else
        {
            movingLeft = false;
        }


        if (inertiaDamping)
        {
            rb.drag = 0.5f;
            rb.angularDrag = 1f;
        }
        else
        {
            rb.drag = 0.2f;
            rb.angularDrag = 0.05f;
        }
        if (Input.GetButtonDown("inertiaDamp"))
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

        if(movingForward)
        {
            forwardMovementImage.color = startColor;
        }
        else
        {
            forwardMovementImage.color = Color.clear;
        }

        if(movingBackward)
        {
            backwardMovementImage.color = startColor;
        }
        else
        {
            backwardMovementImage.color = Color.clear;
        }

        if(movingLeft)
        {
            leftMovementImage.color = startColor;
        }
        else
        {
            leftMovementImage.color = Color.clear;
        }

        if (movingRight)
        {
            rightMovementImage.color = startColor;
        }
        else
        {
            rightMovementImage.color = Color.clear;
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

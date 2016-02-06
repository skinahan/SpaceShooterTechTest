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
    public Image inertiaImage;
    public Image upMovementImage;
    public Image downMovementImage;

    public Slider forwardThrustSlider;
    public Slider backwardThrustSlider;

    private Color startColor;

    private float spawnDistance = 3f;
    public Transform bullet;

    private bool movingForward = false;
    private bool movingRight = false;
    private bool movingLeft = false;
    private bool movingBackward = false;
    private bool movingUp = false;
    private bool movingDown = false;

    private int mainEngineThrust = 0;

    void Start()
    {
        startColor = forwardMovementImage.color;
        rb = GetComponent<Rigidbody>();
    }

    private void checkMovement()
    {
        float forwardVelocity = Vector3.Dot(rb.velocity, transform.forward);
        float rightwardVelocity = Vector3.Dot(rb.velocity, transform.right);
        float upwardVelocity = Vector3.Dot(rb.velocity, transform.up);

        //Vector3 localangularvelocity = transform.InverseTransformDirection(rb.angularVelocity).normalized * rb.angularVelocity.magnitude;
        if (forwardVelocity == 0)
        {
            movingForward = false;
            movingBackward = false;
        }

        if (rightwardVelocity == 0)
        {
            movingRight = false;
            movingLeft = false;
        }

        if (upwardVelocity == 0)
        {
            movingUp = false;
            movingDown = false;
        }

        if (forwardVelocity > 1)
        {
            movingForward = true;
        }
        else
        {
            movingForward = false;
        }

        if (forwardVelocity < -1)
        {
            movingBackward = true;
        }
        else
        {
            movingBackward = false;
        }

        if (upwardVelocity > 1)
        {
            movingUp = true;
        }
        else
        {
            movingUp = false;
        }

        if (upwardVelocity < -1)
        {
            movingDown = true;
        }
        else
        {
            movingDown = false;
        }

        if (rightwardVelocity > 1)
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
    }
	
    void FixedUpdate ()
    {
        checkMovement();

        if (Input.GetButtonDown("inertiaDamp"))
        {
            inertiaDamping = !inertiaDamping;
        }

        if(Input.GetButtonDown("cutEngine"))
        {
            mainEngineThrust = 0;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Quaternion F = transform.rotation;
            GameObject.Instantiate(bullet, transform.position + spawnDistance * transform.forward, F * Quaternion.Euler(90, 0, 0));
        }

        if (inertiaDamping)
        {
            inertiaImage.color = startColor;
            rb.drag = 0.5f;
            rb.angularDrag = 1f;
        }
        else
        {
            inertiaImage.color = Color.clear;
            rb.drag = 0.2f;
            rb.angularDrag = 0.05f;
        }
    }

    private void updateIndicator(Image img, bool status)
    {
        if(status)
        {
            img.color = startColor;
        }
        else
        {
            img.color = Color.clear;
        }
    }

    private void updateMovementIndicators()
    {
        updateIndicator(forwardMovementImage, movingForward);
        updateIndicator(backwardMovementImage, movingBackward);
        updateIndicator(leftMovementImage, movingLeft);
        updateIndicator(rightMovementImage, movingRight);
        updateIndicator(upMovementImage, movingUp);
        updateIndicator(downMovementImage, movingDown);
    }

    private void updateHUD()
    {
        if(mainEngineThrust == 0)
        {
            forwardThrustSlider.value = 0;
            backwardThrustSlider.value = 0;
        }

        if(mainEngineThrust > 0)
            forwardThrustSlider.value = mainEngineThrust;

        if(mainEngineThrust < 0)
            backwardThrustSlider.value = -mainEngineThrust;

        updateMovementIndicators();
    }

    private void handleInput()
    {
        float h = Input.GetAxis("Horizontal") * strafeThrust * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * mainThrust * Time.deltaTime;
        float clockwise = Input.GetAxis("Clockwise") * rotateThrust * Time.deltaTime;
        float x = Input.GetAxis("Jump") * verticalThrust * Time.deltaTime;        


        rb.AddForce(transform.forward * v, ForceMode.Acceleration);
        rb.AddForce(transform.right * h, ForceMode.Acceleration);
        rb.AddForce(transform.up * x, ForceMode.Acceleration);
        rb.AddTorque(-transform.forward * clockwise);
    }

    private void alternateInput()
    {
        float h = Input.GetAxis("Horizontal") * strafeThrust * Time.deltaTime;

        float v = Input.GetAxis("Vertical");

        if(v > 0)
        {
            if(mainEngineThrust < 100)
                mainEngineThrust++;
        }

        if(v < 0)
        {
            if(mainEngineThrust > -100)
                mainEngineThrust--;
        }

        float clockwise = Input.GetAxis("Clockwise") * rotateThrust * Time.deltaTime;

        float x = Input.GetAxis("Jump") * verticalThrust * Time.deltaTime;


        rb.AddForce((transform.forward * mainEngineThrust)/3, ForceMode.Acceleration);
        rb.AddForce(transform.right * h, ForceMode.Acceleration);
        rb.AddForce(transform.up * x, ForceMode.Acceleration);
        rb.AddTorque(-transform.forward * clockwise);
    }

    // Update is called once per frame
    void Update () {
        updateHUD();
        //handleInput();
        alternateInput();
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public float hoverHeight = 3F;
    public float hoverHeightStrictness = 1F;
    public float forwardThrust = 5000F;
    public float backwardThrust = 2500F;
    public float bankAmount = 0.1F;
    public float bankSpeed = 0.2F;
    public Vector3 bankAxis = new Vector3(-1F, 0F, 0F);
    public float turnSpeed = 8000F;

    public Vector3 forwardDirection = new Vector3(1F, 0F, 0F);

    public float mass = 5F;

    // positional drag
    public float sqrdSpeedThresholdForDrag = 25F;
    public float superDrag = 2F;
    public float fastDrag = 0.5F;
    public float slowDrag = 0.01F;

    // angular drag
    public float sqrdAngularSpeedThresholdForDrag = 5F;
    public float superADrag = 32F;
    public float fastADrag = 16F;
    public float slowADrag = 0.1F;

    public bool playerControl = true;

    float bank = 0F;

    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent <Rigidbody> ();
    }

    void SetPlayerControl(bool control) {
        playerControl = control;
    }

    void Start() {
        rb.mass = mass;
        forwardDirection = transform.forward;
    }

    void FixedUpdate() {
        if (Mathf.Abs(thrust) > 0.01F) {
            if (rb.velocity.sqrMagnitude > sqrdSpeedThresholdForDrag)
                rb.drag = fastDrag;
            else
                rb.drag = slowDrag;
        }
        else rb.drag = superDrag;

        if (Mathf.Abs(turn) > 0.01F) {
            if (rb.angularVelocity.sqrMagnitude > sqrdAngularSpeedThresholdForDrag)
                rb.angularDrag = fastADrag;
            else
                rb.angularDrag = slowADrag;
        } else
            rb.angularDrag = superADrag;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hoverHeight, transform.position.z), hoverHeightStrictness);

        float amountToBank = rb.angularVelocity.y * bankAmount;

        bank = Mathf.Lerp(bank, amountToBank, bankSpeed);

        Vector3 rotation = transform.rotation.eulerAngles;
        rotation *= Mathf.Deg2Rad;
        rotation.x = 0F;
        rotation.z = 0F;
        rotation += bankAxis * bank;
        //transform.rotation = Quaternion.EulerAngles(rotation);
        rotation *= Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotation);
    }

    float thrust = 0F;
    float turn = 0F;

    void Thrust(float t) {
        thrust = Mathf.Clamp(t, -1F, 1F);
    }

    void Turn(float t) {
        turn = Mathf.Clamp(t, -1F, 1F) * turnSpeed;
    }

    bool thrustGlowOn = false;

    void Update() {
        float theThrust = thrust;

        if (playerControl) {
            thrust = Input.GetAxis("Vertical");
            turn = Input.GetAxis("Horizontal") * turnSpeed;
        }

        if (thrust > 0F) {
            theThrust *= forwardThrust;
            if (!thrustGlowOn) {
                thrustGlowOn = !thrustGlowOn;
                BroadcastMessage("SetThrustGlow", thrustGlowOn, SendMessageOptions.DontRequireReceiver);
            }
        } else {
            theThrust *= backwardThrust;
            if (thrustGlowOn) {
                thrustGlowOn = !thrustGlowOn;
                BroadcastMessage("SetThrustGlow", thrustGlowOn, SendMessageOptions.DontRequireReceiver);
            }
        }

        rb.AddRelativeTorque(Vector3.up * turn * Time.deltaTime);
        rb.AddRelativeForce(forwardDirection * theThrust * Time.deltaTime);
    }
}
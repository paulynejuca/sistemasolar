using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class SpaceshipController : MonoBehaviour {

    [SerializeField] private float forwardThrust;                   // Força do impulso
    [SerializeField] private float turnSpeed;                       // Velocidade de rotação Y
    [SerializeField] private float rotateSpeed;                     // Velocidade de rotação Z

    private float horizontal = 0f;                                  // Entradas no eixo horizontal
    private float vertical = 0f;                                    // Entradas no eixo vertical
    private float thrust = 0f;                                      // Entradas no eixo de impulso
    private float rotate = 0f;                                      // Entradas no eixo de rotação

    // drag posicional
    [SerializeField] private float sqrdSpeedThresholdForDrag = 25F;
    [SerializeField] private float fastDrag = 0.5F;
    [SerializeField] private float slowDrag = 0.01F;

    [SerializeField] private float fuelLevel;                       // Combustível inicial
    private float fuel;                                             // Combustível atual
    [SerializeField] private float fuelConsume;                     // Consumo de combustível
    [SerializeField] private Slider fuelSlider;                     // Elemento de UI que exibe o nível do combustível

    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        fuel = fuelLevel;
        fuelSlider.value = fuelLevel;
        fuelSlider.maxValue = fuelLevel;
        fuelSlider.minValue = 0f;
        fuelSlider.direction = Slider.Direction.RightToLeft;
    }

    private void Update () {
        GetInputs();
        //Thrust();
        Turn();
        Rotate();
	}

    private void FixedUpdate() {
        Thrust();

        if (Mathf.Abs(thrust) > 0.01f) {
            if (rb.velocity.sqrMagnitude > sqrdSpeedThresholdForDrag)
                rb.drag = fastDrag;
            else
                rb.drag = slowDrag;
        }
    }

    // Pega entradas
    private void GetInputs() {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        thrust = Input.GetAxis("Thrust");
        rotate = Input.GetAxis("Rotate");
    }

    private void Thrust() {
        if (thrust > 0.01f) {
            //transform.Translate(Vector3.forward * forwardThrust * Time.deltaTime);
            rb.AddRelativeForce(Vector3.forward * forwardThrust * Time.fixedDeltaTime);
            ConsumeFuel();
        }
    }

    private void Turn() {
        if (horizontal > 0.01f)
            transform.Rotate(Vector3.up * horizontal * turnSpeed * Time.deltaTime);

        if (horizontal < 0.01f)
            transform.Rotate(Vector3.down * (-horizontal) * turnSpeed * Time.deltaTime);

        if (vertical > 0.01f)
            transform.Rotate(Vector3.right * (-vertical) * turnSpeed * Time.deltaTime);

        if (vertical < 0.01f)
            transform.Rotate(Vector3.left * vertical * turnSpeed * Time.deltaTime);
    }

    private void Rotate() {
        if (rotate > 0.01f)
            transform.Rotate(Vector3.forward * (-rotate) * rotateSpeed * Time.deltaTime);

        if (rotate < 0.01f)
            transform.Rotate(Vector3.back * rotate * rotateSpeed * Time.deltaTime);
    }


    private void ConsumeFuel() {
        fuel = fuel - (fuelConsume * Time.deltaTime);
        fuelSlider.value = fuel;

        if (fuel <= 0) {
            // Game Over
            Debug.Log("Game Over");
        }
    }
}

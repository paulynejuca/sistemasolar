using UnityEngine;

public class PlanetScript: MonoBehaviour {

    public enum Direction {
        Right, Left, Up, Down, UpRight, UpLeft, DownRight, DownLeft
    }

    public Transform largePlanet;
    public float largePlanetMass;
    public float smallPlanetMass;
    public Direction direction = Direction.Right;

    private Vector3 distance;
    private float initVelocity = 0f;
    private Rigidbody rbLargePlanet;
    private Rigidbody rbSmallPlanet;
    private Transform smallPlanet;

    private void Start() {
        rbSmallPlanet = GetComponent<Rigidbody>();
        smallPlanet = transform;

        // Calcula a velocidade inicial
        //O cálculo usado é o da velicidade de escape Ve = sqrt(2.G.M)
        // Onde Ve = velocidade de escape
        // G = constante gravitacional
        // M = massa do planeta
        initVelocity = Mathf.Sqrt(100f * Constants.G * largePlanetMass / smallPlanet.position.magnitude);

        // Aplica a velocidade incial
        switch (direction) {
            case Direction.Right: rbSmallPlanet.velocity += initVelocity * Vector3.right; break;
            case Direction.Left: rbSmallPlanet.velocity += initVelocity * Vector3.left; break;
            case Direction.Up: rbSmallPlanet.velocity += initVelocity * Vector3.up; break;
            case Direction.Down: rbSmallPlanet.velocity += initVelocity * Vector3.down; break;

            case Direction.UpRight: rbSmallPlanet.velocity += initVelocity * new Vector3(1, 1, 0); break;
            case Direction.UpLeft: rbSmallPlanet.velocity += initVelocity * new Vector3(-1, 1, 0); break;
            case Direction.DownRight: rbSmallPlanet.velocity += initVelocity * new Vector3(1, -1, 0); break;
            case Direction.DownLeft: rbSmallPlanet.velocity += initVelocity * new Vector3(-1, -1, 0); break;

            default: rbSmallPlanet.velocity += initVelocity * Vector3.right; break;
        }
    }

    private void FixedUpdate() {
        // Calcula a distãncia entre os dois planetas
        float r = Vector3.Distance(smallPlanet.position, largePlanet.position);
        // Calcula a força
        float totalForce = -(Constants.G * largePlanetMass * smallPlanetMass) / (r * r);
        Vector3 force = (smallPlanet.position - largePlanet.position).normalized * totalForce;
        // Aplica a força
        rbSmallPlanet.AddForce(force);

        if (r <= (largePlanet.GetComponent<SphereCollider>().radius + smallPlanet.GetComponent<SphereCollider>().radius) + 10f) {
            smallPlanet.gameObject.SetActive(false);
        }
    }

}

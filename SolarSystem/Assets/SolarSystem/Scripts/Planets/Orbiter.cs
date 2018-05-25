using System.Collections;
using UnityEngine;

//==============================//
//===        Orbiter         ===//
//==============================//

/*
  Required component. Add Orbiter.js to the object that you would like to put into orbit.
 
  Dependencies:
    OrbitalEllipse.js - calculates the shape, orientation, and offset of an orbit
    OrbitState.js - calculates the initial state of the orbiter
*/

[RequireComponent(typeof(Rigidbody))]
public class Orbiter : MonoBehaviour{ 

    public Transform orbitAround;
    public float orbitSpeed; // In the original orbital equations this is gravity, not speed
    public float apsisDistance; // By default, this is the periapsis (closest point in its orbit)
    public float startingAngle = 0; // 0 = starting apsis, 90 = minor axis, 180 = ending apsis
    public bool circularOrbit = false;
    public bool counterclockwise = false;
 
    private float gravityConstant = 100;
    private Rigidbody rb;
    private Transform trans;
    private OrbitalEllipse ellipse;
    private OrbitState orbitState;
 
    // Accessor
    public OrbitalEllipse Ellipse() {
	    return ellipse;
    }

        public Transform Transform()  {
	    return trans;
    }
    public float GravityConstant() {
	    return gravityConstant;
    }
 
 
    // Setup the orbit when the is added
    private void Reset() {
        if (orbitAround == null)
            return;
        ellipse = new OrbitalEllipse(orbitAround.position, transform.position, apsisDistance, circularOrbit);
        apsisDistance = ellipse.endingApsis; // Default to a circular orbit by setting both apses to the same value
    }

    private void OnApplicationQuit() {
        ellipse = new OrbitalEllipse(orbitAround.position, transform.position, apsisDistance, circularOrbit);
    }

    private void OnDrawGizmosSelected() {
        if (orbitAround == null)
            return;
        // This is required for the OrbitRenderer. For some reason the ellipse var is always null
        // if it's set anywhere else, even including OnApplicationQuit;
        if (ellipse == null)
            ellipse = new OrbitalEllipse(orbitAround.position, transform.position, apsisDistance, circularOrbit);
        // Never allow 0 apsis. Start with a circular orbit.
        if (apsisDistance == 0) {
            apsisDistance = ellipse.startingApsis;
        }
    }


    private void Start() {
        // Cache transform
        trans = transform;
        // Cache & set up rigidbody
        rb = GetComponent<Rigidbody>();
        rb.drag = 0;
        rb.useGravity = false;
        rb.isKinematic = false;

        // Bail out if we don't have an object to orbit around
        if (orbitAround == null) {
            Debug.LogWarning("Satellite has no object to orbit around");
            return;
        }

        // Update the ellipse with initial value
        if (ellipse == null)
            Reset();
        ellipse.Update(orbitAround.position, transform.position, apsisDistance, circularOrbit);

        // Calculate starting orbit state
        orbitState = new OrbitState(startingAngle, this, ellipse);

        // Position the orbiter
        trans.position = ellipse.GetPosition(startingAngle, orbitAround.position);

        // Add starting velocity
        rb.AddForce(orbitState.velocity, ForceMode.VelocityChange);
        StartCoroutine("Orbit");
    }

// Coroutine to apply gravitational forces on each fixed update to keep the object in orbit
    private IEnumerator Orbit() {
        while (true) {
            // Debug.DrawLine(orbitState.position - orbitState.tangent*4, orbitState.position + orbitState.tangent*4);
            var difference = trans.position - orbitAround.position;
            rb.AddForce(-difference.normalized * orbitSpeed * gravityConstant * Time.fixedDeltaTime / difference.sqrMagnitude, ForceMode.VelocityChange);
            yield return new WaitForFixedUpdate();
        }
    }
}
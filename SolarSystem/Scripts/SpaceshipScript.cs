using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipScript : MonoBehaviour {

    public float speed = 10f;

    private Rigidbody rb;

	private void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void Update() {
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.up, -2f);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up, 2f);
        }

        if (Input.GetKey(KeyCode.W)) {
            transform.Rotate(Vector3.right, 2f);
        }

        if (Input.GetKey(KeyCode.S)) {
            transform.Rotate(Vector3.right, -2f);
        }
    }

    private void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.N)) 
            MoveFoward();
        
        if (Input.GetKeyDown(KeyCode.M))
            Stop();
	}

    private void MoveFoward() {
        rb.velocity = transform.forward * speed;
    }

    private void Stop() {
        rb.velocity = Vector3.zero;
    }
}

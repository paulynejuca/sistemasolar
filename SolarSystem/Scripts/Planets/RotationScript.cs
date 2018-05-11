using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {

    public float speed;

	private void Update () {
        transform.Rotate(Vector3.up, speed, Space.World);
	}

}

using UnityEngine;
using System.Collections;

public class SmoothFollowScript : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 3.0f;
    [SerializeField] private float height = 3.0f;
    [SerializeField] private float damping = 5.0f;
    [SerializeField] private bool smoothRotation = true;
    [SerializeField] private float rotationDamping = 10.0f;

    private void FixedUpdate() {
        Vector3 wantedPosition = target.TransformPoint(0, height, -distance);
        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        if (smoothRotation) {
            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        } else {
            transform.LookAt(target, target.up);
        }
    }
}
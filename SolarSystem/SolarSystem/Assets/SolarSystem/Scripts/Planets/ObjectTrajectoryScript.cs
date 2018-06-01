using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ObjectTrajectoryScript : MonoBehaviour {

    public Color startColor = Color.blue;
    public Color endColor = Color.white;
    public float startWidth = .5f;
    public float endWidth = .5f;
    public float timeToDraw = .5f;
    //public int pointsTodraw = 60;

    private float currentTime = 0f;
    private LineRenderer lineRenderer;
    private List<Vector3> positions;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        positions = new List<Vector3>();
        positions.Add(transform.position);
    }

    private void Start() {
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = startColor;
        lineRenderer.endColor = endColor;
        lineRenderer.startWidth = startWidth;
        lineRenderer.endWidth = endWidth;
    }

    private void Update() {
        lineRenderer.positionCount = positions.Count;

        for (int i = 0; i < positions.Count; i++) {
            lineRenderer.SetPosition(i, positions[i]);
        }

        if (timeToDraw >= currentTime) {
            currentTime += Time.deltaTime;
        } else {
            currentTime = 0f;
            positions.Add(transform.position);
        }
    }


}

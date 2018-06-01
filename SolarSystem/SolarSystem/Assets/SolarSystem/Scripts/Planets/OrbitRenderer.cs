using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//===============================//
//===     Orbit Renderer      ===//
//===============================//

[RequireComponent(typeof(Orbiter))]
public class OrbitRenderer : MonoBehaviour {
    /*
      Optional component. Display the Orbiter component's properties in the editor. Does nothing in-game.
    */

    public Color orbitPointsColor = new Color(1,1,0,0.5f); // Yellow
    public float orbitPointsSize = 0.5f;
    public float ellipseResolution = 24f;
    //var renderAsLines : boolean = false;
 
    public Color startPointColor = new Color(1,0,0,0.7f); // Red
    public float startPointSize = 1.0f;
 
    private Orbiter orbiter;
    private OrbitalEllipse ellipse;
 
    private void Awake() {
        // Remove the component in the compiled game. Likely not a noticeable optimization, just an experiment.
        if (!Application.isEditor)
            Destroy(this);
    }

    private void Reset() {
        orbiter = GetComponent<Orbiter>();
    }

    private void OnApplicationQuit() {
        orbiter = GetComponent<Orbiter>();
    }


    private void OnDrawGizmosSelected() {
        if (orbiter == null)
            orbiter = GetComponent<Orbiter>();

        // Bail out if there is no object to orbit around
        if (!orbiter.orbitAround)
            return;

        // Recalculate the ellipse only when in the editor
        if (!Application.isPlaying) {
            if (orbiter.Ellipse() == null)
                return;
            orbiter.Ellipse().Update(orbiter.orbitAround.position, transform.position, orbiter.apsisDistance, orbiter.circularOrbit);
        }

        DrawEllipse();
        DrawStartingPosition();
    }

    public void DrawEllipse() {
        for (float angle = 0; angle < 360; angle += 360 / ellipseResolution) {
            Gizmos.color = orbitPointsColor;
            Gizmos.DrawSphere(orbiter.Ellipse().GetPosition(angle, orbiter.orbitAround.position), orbitPointsSize);
        }
    }

    public void  DrawStartingPosition() {
        Gizmos.color = startPointColor;
        Gizmos.DrawSphere(orbiter.Ellipse().GetPosition(orbiter.startingAngle, orbiter.orbitAround.position), startPointSize);
    }
}

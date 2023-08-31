using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    public static GravityManager Instance { get; private set; }

    [SerializeField] float g = 1f;
    static float G;
    //in physical universe every body would be both attractor and attractee
    public static List<Rigidbody2D> attractors = new List<Rigidbody2D>();
    public static List<Rigidbody2D> attractees = new List<Rigidbody2D>();
    public static bool isSimulatingLive = true;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    void FixedUpdate() {
        G = g;//in case g is changed in editor
        if (isSimulatingLive)//PathHandler changes this
            SimulateGravities();
    }
    public static void SimulateGravities() {
        foreach (Rigidbody2D attractor in attractors) {
            foreach (Rigidbody2D attractee in attractees) {
                if (attractor != attractee)
                    AddGravityForce(attractor, attractee);
            }
        }
    }

    public static void AddGravityForce(Rigidbody2D attractor, Rigidbody2D target) {
        float massProduct = attractor.mass * target.mass * G;

        //You could also do
        //float distance = Vector3.Distance(attractor.position,target.position.
        Vector3 difference = attractor.position - target.position;
        float distance = difference.magnitude; // r = Mathf.Sqrt((x*x)+(y*y))

        //F = G * ((m1*m2)/r^2)
        float unScaledforceMagnitude = massProduct / Mathf.Pow(distance, 2);
        float forceMagnitude = G * unScaledforceMagnitude;

        Vector3 forceDirection = difference.normalized;

        Vector3 forceVector = forceDirection * forceMagnitude;

        target.AddForce(forceVector);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Orbital : MonoBehaviour {

    List<Planet> planets;

    private void Awake() {
        planets = new List<Planet>();
    }
    
    void Start () {
        planets.AddRange(FindObjectsOfType<Planet>());
	}

    private void FixedUpdate() {
        foreach (var p in planets)
        {
            var vector = p.transform.position - transform.position;
            var radius = vector.magnitude;
            if (radius < p.influenceRadius)
            {
                var gravity = (p.mass + GetComponent<Rigidbody2D>().mass) / (radius * radius);
                GetComponent<Rigidbody2D>().AddForce(vector.normalized * gravity);
                Debug.DrawLine(transform.position, transform.position + vector.normalized * gravity);
            }
        }
    }
}

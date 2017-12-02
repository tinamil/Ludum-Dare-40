using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public int numAsteroids;
    public float radius;
    public GameObject[] Asteroids;

	// Use this for initialization
	void Start () {
	
        for(int i = 0; i < numAsteroids; ++i)
        {
            Vector3 position = Random.insideUnitCircle * radius;
            Quaternion rotation = Random.rotation;
            Instantiate(Asteroids[Random.Range(0, Asteroids.Length)], position, rotation);
        }
        
	}
	
}

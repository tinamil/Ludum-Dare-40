using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeThis : MonoBehaviour {

    public float ExplosionForce;

	// Use this for initialization
	void Start () {
        var c = GetComponentsInChildren<Rigidbody2D>();
        foreach(var child in c)
        {
            child.AddForce(Random.onUnitSphere * ExplosionForce, ForceMode2D.Impulse);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

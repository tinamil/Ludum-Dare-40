using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMovement : MonoBehaviour {

    public GameObject target;
    public float ratio;

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = target.transform.position * ratio;
	}
}

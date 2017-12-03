using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyAt : MonoBehaviour {

    public string targetTag;
    public Vector3 forward;

    private GameObject target;

    private void FixedUpdate() {
        if (target == null && targetTag != null)
        {
            target = GameObject.FindGameObjectWithTag(targetTag);
        }
        if(target != null)
        {
            var body = GetComponent<Rigidbody2D>();
            var direction =  target.transform.position - transform.position;
            body.AddForce(direction);
            body.rotation = (body.rotation + Vector2.SignedAngle(transform.TransformDirection(forward), direction));
        }
    }
}

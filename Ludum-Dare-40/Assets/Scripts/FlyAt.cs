using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyAt : MonoBehaviour {

    public string targetTag;

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
            body.rotation = (body.rotation + Vector2.SignedAngle(transform.right, direction));
        }
    }
}

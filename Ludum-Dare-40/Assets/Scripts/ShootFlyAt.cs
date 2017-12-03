using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFlyAt : MonoBehaviour
{

    public string targetTag;
    public Vector3 forward;
    public GameObject weapon;
    public float weaponSpeed;
    public float fireRate;

    private GameObject target;
    private float lastFired;

    private void FixedUpdate() {
        if (target == null && targetTag != null)
        {
            target = GameObject.FindGameObjectWithTag(targetTag);
        }
        if (target != null)
        {
            var body = GetComponent<Rigidbody2D>();
            var targetBody = target.GetComponent<Rigidbody2D>();

            var targetPosition = targetBody.position + Random.insideUnitCircle;// + targetBody.velocity * 2;
            var direction = targetPosition - body.position;

            body.AddForce(direction);
            body.rotation = (body.rotation + Vector2.SignedAngle(transform.TransformDirection(forward), direction));

            if (lastFired + fireRate < Time.fixedTime)
            {
                lastFired = Time.fixedTime;
                var p = Instantiate(weapon, transform.position, transform.rotation);
                p.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + weaponSpeed * direction;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    public float Engine = 10f;
    public float Torque = 10f;


    private void Awake() {
    }

    void OnEnable() {

    }

    void OnDisable() {

    }

    void FixedUpdate() {
        Vector2 movement = Vector2.zero;
        if (InputManager.IsFiring(InputManager.InputEvent.Forward))
        {
            movement.x += 1;
        }
        if (InputManager.IsFiring(InputManager.InputEvent.Backward))
        {
            movement.x -= 1;
        }
        if (InputManager.IsFiring(InputManager.InputEvent.Left))
        {
            movement.y += 1;
        }
        if (InputManager.IsFiring(InputManager.InputEvent.Right))
        {
            movement.y -= 1;
        }
        Move(movement);

        var worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Orient(worldMouse);
    }

    void Move(Vector2 vec) {
        GetComponent<Rigidbody2D>().AddRelativeForce(vec * Engine);
    }

    void Orient(Vector2 vec) {
        var body = GetComponent<Rigidbody2D>();
        var angularVelocity = body.angularVelocity;
        var distance = Vector2.SignedAngle(transform.right, vec - (Vector2)transform.position);
        var angularAcceleration = Torque / GetComponent<Rigidbody2D>().inertia;
        var maxVelChange = angularAcceleration * Time.fixedDeltaTime;
        var maxDisThisDelta = 0.5f * maxVelChange * Time.fixedDeltaTime;

        if (Mathf.Abs(angularVelocity) <= Mathf.Epsilon && Mathf.Abs(distance) <= Mathf.Epsilon) return;

        if (Mathf.Abs(angularVelocity) < maxVelChange && Mathf.Abs(distance) < maxDisThisDelta)
        {
            body.MoveRotation(body.rotation + distance);
            body.angularVelocity = 0;
            return;
        }
        if (Mathf.Abs(angularVelocity) <= Mathf.Epsilon && Mathf.Abs(distance) >= maxDisThisDelta)
        {
            body.AddTorque(Mathf.Sign(distance) * Torque);
            return;
        }

        var stopDistance = 0.5 * angularVelocity * angularVelocity / angularAcceleration * Time.deltaTime;
        var direction = Mathf.Sign(distance);
        if (stopDistance < Mathf.Abs(distance))
        {
            if (distance < maxDisThisDelta * 4)
            {
                body.AddTorque(Mathf.Sign(distance) * Torque / 2);
            } else
            {
                body.AddTorque(Mathf.Sign(distance) * Torque);
            }
        } else
        {
            body.AddTorque(Mathf.Sign(angularVelocity) * -Torque);
        }
        
    }

}

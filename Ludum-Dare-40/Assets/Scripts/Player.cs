using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    public float Engine = 10f;
    public float Torque = 10f;

    void OnEnable() {
    }

    void OnDisable() {

    }

    void Update() {
        Vector2 movement = Vector2.zero;
        if (InputManager.IsFiring(InputManager.InputEvent.Forward))
        {
            movement.y += 1;
        }
        if (InputManager.IsFiring(InputManager.InputEvent.Backward))
        {
            movement.y -= 1;
        }
        if (InputManager.IsFiring(InputManager.InputEvent.Left))
        {
            movement.x -= 1;
        }
        if (InputManager.IsFiring(InputManager.InputEvent.Right))
        {
            movement.x += 1;
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
        var distance = Vector2.SignedAngle(transform.right, vec);
        if (angularVelocity == 0 && distance == 0) return;
        var inertia = GetComponent<Rigidbody2D>().inertia;
        var angularAcceleration = Torque / inertia;
        var maxVelChange = angularAcceleration * Time.fixedDeltaTime;
        var maxDisPerDelta = 0.5f * maxVelChange * Time.fixedDeltaTime;
        if (Mathf.Abs(angularVelocity) < maxVelChange && Mathf.Abs(distance) < maxDisPerDelta)
        {
            body.MoveRotation(distance);
            body.AddTorque(-body.angularVelocity * body.inertia, ForceMode2D.Impulse);
            return;
        }
        if (Mathf.Abs(angularVelocity) <= Mathf.Epsilon && Mathf.Abs(distance) >= maxDisPerDelta)
        {
            body.AddTorque(Mathf.Sign(distance) * Torque);
            return;
        }

        var timePerSpin = 360 / Mathf.Abs(angularVelocity);
        if(Mathf.Abs(angularVelocity) > 0.5f * angularAcceleration * timePerSpin)
        {
            body.AddTorque(Mathf.Sign(angularVelocity) * -Torque);
            return;
        }

        var timeToArrival = 2 * distance / angularVelocity;
        if(timeToArrival < 0)
        {
            body.AddTorque(Mathf.Sign(distance) * Torque);
            return;
        }
        var maxStopVel = angularAcceleration * timeToArrival;
        if (Mathf.Abs(angularVelocity) > maxStopVel)
        {
            body.AddTorque(Mathf.Sign(distance) * -Torque);
        } else
        {
            body.AddTorque(Mathf.Sign(distance) * Torque);
        }
    }
    
}

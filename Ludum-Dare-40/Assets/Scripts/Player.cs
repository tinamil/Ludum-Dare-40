using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public ParticleSystem engineFlare;
    public float Engine = 10f;
    public float Torque = 10f;
    public GameObject weaponHardPoint;
    public float FireDelay = 10;
    public Laser weapon;
    public AudioClip[] laserEffects;

    private float lastFired;

    private void Awake() {
        lastFired = -1;
    }

    void OnEnable() {
        InputManager.AddAction(InputManager.InputEvent.Fire, Fire);
    }

    void OnDisable() {
        InputManager.RemoveAction(InputManager.InputEvent.Fire, Fire);
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
        var emission = engineFlare.emission;
        emission.rateOverTimeMultiplier = vec.normalized.magnitude * 100;
        var shape = engineFlare.shape;
        var dir = vec.normalized;
        shape.rotation = new Vector3(dir.y, -dir.x, 0) * 90;
        GetComponent<AudioSource>().volume = dir.normalized.magnitude;
    }

    void Orient(Vector2 vec) {
        var body = GetComponent<Rigidbody2D>();
        var angularVelocity = body.angularVelocity;
        var distance = Vector2.SignedAngle(transform.right, vec - (Vector2)transform.position);
        var angularAcceleration = Torque * Mathf.Rad2Deg / GetComponent<Rigidbody2D>().inertia;
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

        var stopDistance = 0.5 * angularVelocity * angularVelocity / angularAcceleration;
        var deltaVel = distance / Time.fixedDeltaTime;
        var direction = Mathf.Sign(distance);
        if (stopDistance < Mathf.Abs(distance))
        {
            body.AddTorque(Mathf.Sign(distance) * Torque);
        } else if (Mathf.Abs(deltaVel) < Torque * Time.fixedDeltaTime)
        {
            body.AddTorque(Mathf.Sign(distance) * (deltaVel - angularVelocity));
        } else
        {
            body.AddTorque(Mathf.Sign(angularVelocity) * -Torque);
        }

    }

    void Fire() {
        if (lastFired + FireDelay < Time.fixedTime)
        {
            var laser = Instantiate(weapon, weaponHardPoint.transform.position, transform.rotation, null);
            laser.SetStartSpeed(GetComponent<Rigidbody2D>().velocity);
            GetComponent<AudioSource>().PlayOneShot(laserEffects[Random.Range(0, laserEffects.Length)]);
            lastFired = Time.fixedTime;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public ParticleSystem engineFlare;
    public float Engine = 10f;
    public float Torque = 10f;
    public GameObject[] weaponHardPoints;
    public float FireDelay = 10;
    public Laser weapon;
    public GameObject shield;
    public AudioClip[] laserEffects;
    public AudioSource laserAudioSource;
    public int HealthCapacity = 10;
    public int LaserCapacity = 100;
    public float LaserRechargeRate = .2f;
    public float LaserRechargeDelay = 1;

    public int ShieldCapacity = 2;
    public int ShieldRechargeRate = 5;

    public TextMeshProUGUI HPText;
    public RectTransform ShieldUI; //400px height each
    public RectTransform LaserUI;
    public RectTransform HealthUI;

    public AudioClip powerup;

    private float lastFired;
    private float lastLaserRecharge;
    private int lastFiredPoint = 0;
    private int laserCharge;

    private float lastShieldRecharge;
    private int shieldCharge;
    private float lastDamageTaken;
    private float immunity = 0.2f;

    private int HPRemaining;

    private void Awake() {
        lastFired = -1;
        laserCharge = LaserCapacity;
        shieldCharge = ShieldCapacity;
        HPRemaining = HealthCapacity;
    }

    private void Update() {
        LaserUI.sizeDelta = new Vector2(0, (float)laserCharge / LaserCapacity * 400);
        ShieldUI.sizeDelta = new Vector2(0, (float)shieldCharge / ShieldCapacity * 400);
        HealthUI.sizeDelta = new Vector2(0, (float)HPRemaining / HealthCapacity * 400);
        shield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (float)shieldCharge / ShieldCapacity);
        shield.SetActive(shieldCharge > 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collided with " + collision.gameObject.name);
        if ("Enemy".Equals(collision.gameObject.tag))
        {
            TakeDamage(collision.gameObject.GetComponent<Enemy>().Damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ("HealthPickup".Equals(collision.gameObject.tag))
        {
            MusicController.PlayClip(powerup);
            HPRemaining = Mathf.Clamp(HPRemaining + 1, 0, HealthCapacity);
            Destroy(collision.gameObject);
        }
        if ("BoltPickup".Equals(collision.gameObject.tag))
        {
            MusicController.PlayClip(powerup);
            laserCharge = LaserCapacity;
            Destroy(collision.gameObject);
        }
        if ("Star".Equals(collision.gameObject.tag))
        {
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage) {
        if (lastDamageTaken + immunity < Time.fixedTime)
        {
            if (shieldCharge > 0)
            {
                shieldCharge -= damage;
            } else
            {
                HPRemaining -= damage;
                if (HPRemaining <= 0)
                {
                    GameController.Endgame();
                }
            }
            lastDamageTaken = Time.fixedTime;
        }

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

        if (InputManager.IsFiring(InputManager.InputEvent.Fire))
        {
            Fire();
        }
        if (lastFired + LaserRechargeDelay < Time.fixedTime && lastLaserRecharge + LaserRechargeRate < Time.fixedTime)
        {
            lastLaserRecharge = Time.fixedTime;
            laserCharge = Mathf.Clamp(laserCharge + 1, 0, LaserCapacity);
        }
        if (lastShieldRecharge + ShieldRechargeRate < Time.fixedTime)
        {
            lastShieldRecharge = Time.fixedTime;
            shieldCharge = Mathf.Clamp(shieldCharge + 1, 0, ShieldCapacity);
        }
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
        var maxDegreesThisDelta = 0.5f * maxVelChange * Time.fixedDeltaTime;
        //Debug.Log("Angular velocity = " + angularVelocity + " accel = " + angularAcceleration + " Time = " + Time.fixedTime + " distance = " + Vector2.Angle(Vector2.right, transform.right));
        if (Mathf.Abs(angularVelocity) <= Mathf.Epsilon && Mathf.Abs(distance) <= Mathf.Epsilon) return;

        if (Mathf.Abs(angularVelocity) < maxVelChange && Mathf.Abs(distance) < maxDegreesThisDelta)
        {
            body.MoveRotation(body.rotation + distance);
            body.angularVelocity = 0;
            return;
        }
        if (Mathf.Abs(angularVelocity) <= Mathf.Epsilon && Mathf.Abs(distance) >= maxDegreesThisDelta)
        {
            body.AddTorque(Mathf.Sign(distance) * Torque);
            return;
        }

        var stopDistance = 0.5 * angularVelocity * angularVelocity / angularAcceleration;
        var deltaVel = distance / Time.fixedDeltaTime;

        if (stopDistance < Mathf.Abs(distance))
        {
            //Debug.Log("Accelerating");
            body.AddTorque(Mathf.Sign(distance) * Torque);
        } else if (Mathf.Abs(deltaVel) < Torque * Time.fixedDeltaTime)
        {
            //Debug.Log("Accelerating slowly");
            body.AddTorque(Mathf.Sign(distance) * (deltaVel - angularVelocity));
        } else
        {
            //Debug.Log("Decelerating");
            body.AddTorque(Mathf.Sign(angularVelocity) * -Torque);
        }
    }

    void Fire() {
        if (lastFired + FireDelay < Time.fixedTime && laserCharge > 0)
        {
            lastFiredPoint = (lastFiredPoint + 1) % weaponHardPoints.Length;
            var pos = weaponHardPoints[lastFiredPoint].transform.position;
            var laser = Instantiate(weapon, pos, transform.rotation, null);
            laser.SetStartSpeed(GetComponent<Rigidbody2D>().velocity);
            laserAudioSource.PlayOneShot(laserEffects[UnityEngine.Random.Range(0, laserEffects.Length)]);
            lastFired = Time.fixedTime;
            laserCharge -= 1;
        }
    }
}

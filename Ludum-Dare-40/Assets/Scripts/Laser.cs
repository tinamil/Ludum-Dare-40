using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{

    public float speed;
    public int damage;
    public float lifetime;

    private Vector2 startSpeed = Vector2.zero;


    public void SetStartSpeed(Vector2 speed) {
        startSpeed = speed;
    }

    private void Start() {
        var body = GetComponent<Rigidbody2D>();
        body.velocity = (Vector2)(transform.right * (speed)) + startSpeed;
        StartCoroutine(DestroyThis(lifetime));
    }

    IEnumerator DestroyThis(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        var body = collision.attachedRigidbody;
        if (body != null)
        {
            var enemy = body.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
            if ("Asteroid".Equals(body.gameObject.tag))
            {
                Destroy(gameObject);
            }
        }
    }
}

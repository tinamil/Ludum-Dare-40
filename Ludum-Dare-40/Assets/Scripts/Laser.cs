using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{

    public float speed;
    public float damage;
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

    // Update is called once per frame
    void FixedUpdate() {

    }

    IEnumerator DestroyThis(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}

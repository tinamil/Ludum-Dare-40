using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public float smoothTime;
    public float maxSpeed;
    private Vector2 velocity = Vector2.zero;
    

    // Update is called once per frame
    void Update() {
        var targetPos = Vector2.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothTime, maxSpeed, Time.deltaTime);
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
    }
}

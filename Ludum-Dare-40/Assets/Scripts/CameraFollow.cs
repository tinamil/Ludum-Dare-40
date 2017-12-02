using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject target;
    public float smoothTime;
    public float maxSpeed;
    public float scrollSpeed;
    private Vector2 velocity = Vector2.zero;


    // Update is called once per frame
    void LateUpdate() {
        var targetPos = Vector2.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothTime, maxSpeed, Time.deltaTime);
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        if (Input.mouseScrollDelta.y != 0)
        {
            var delta = GetComponent<Camera>().orthographicSize - Input.mouseScrollDelta.y * scrollSpeed;
            GetComponent<Camera>().orthographicSize = Mathf.Clamp(delta, 5, 300);
        }
    }
}

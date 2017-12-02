using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

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
    }

    void Move(Vector2 vec) {
        GetComponent<Rigidbody2D>().AddRelativeForce(vec);
    }
}

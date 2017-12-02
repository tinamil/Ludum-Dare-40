using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    public float Delay = 0f;

    private void Start() {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}

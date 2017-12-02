using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightPulse : MonoBehaviour {

    public float pulseRate;
    private float strength = 0;
    private float two_pi = Mathf.PI * 2;

    private void Start() {
        StartCoroutine(Pulse());
    }

    IEnumerator Pulse() {
        while (true)
        {
            strength += (pulseRate * Time.deltaTime) % two_pi;
            GetComponent<Light>().intensity = (Mathf.Sin(strength) + 1) / 2;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOut : MonoBehaviour
{

    public float seconds;

    private float startTime;

    public void Fade() {
        StartCoroutine(FadeRoutine());
    }

    public void UnFade() {
        foreach (var c in GetComponentsInChildren<Image>(true))
        {
            c.gameObject.SetActive(true);
            var color = c.color;
            color.a = 1;
            c.color = color;
        }
        foreach (var c in GetComponentsInChildren<TextMeshProUGUI>(true))
        {
            c.gameObject.SetActive(true);
            var color = c.color;
            color.a = 1;
            c.color = color;
        }
    }

    IEnumerator FadeRoutine() {
        startTime = Time.time;
        while (Time.time - startTime <= seconds)
        {
            foreach (var c in GetComponentsInChildren<Image>())
            {
                var color = c.color;
                color.a = Mathf.Clamp01(1 - ((Time.time - startTime) / seconds));
                c.color = color;
            }
            foreach (var c in GetComponentsInChildren<TextMeshProUGUI>())
            {
                var color = c.color;
                color.a = Mathf.Clamp01(1 - ((Time.time - startTime) / seconds));
                c.color = color;
            }
            yield return null;
        }
        foreach (var c in GetComponentsInChildren<Image>())
        {
            c.gameObject.SetActive(false);
        }
        foreach (var c in GetComponentsInChildren<TextMeshProUGUI>())
        {
            c.gameObject.SetActive(false);
        }
    }
}

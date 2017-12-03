using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{

    public float inputDelay = 1f;

    private float startTime;

    // Use this for initialization
    void OnEnable() {
        GameController.Pause();
        startTime = Time.unscaledTime;
    }

    // Update is called once per frame
    void Update() {
        if (startTime + inputDelay < Time.unscaledTime && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}

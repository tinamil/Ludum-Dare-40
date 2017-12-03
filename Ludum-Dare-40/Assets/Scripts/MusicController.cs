using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour {

    public AudioClip[] mediumMusic;
    public AudioClip[] fastMusic;
    static MusicController instance;

    private void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start () {
        GetComponent<AudioSource>().clip = mediumMusic[Random.Range(0, mediumMusic.Length)];
        GetComponent<AudioSource>().Play();
        StartCoroutine(MonitorMusic());
    }

    IEnumerator MonitorMusic() {
        while (true)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().clip = fastMusic[Random.Range(0, mediumMusic.Length)];
                GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSecondsRealtime(10);
        }
    }

    public static void PlayClip(AudioClip clip) {
        instance.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}

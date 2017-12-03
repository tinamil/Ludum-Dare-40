using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public FadeOut Menu;
    public GameObject EndGame;

    private void Awake() {
        instance = this;
    }

    void Start () {
        Pause();
	}
	
    public void StartGame() {
        Resume();
    }

    public static void Pause() {
        Time.timeScale = 0;
    }

    public static void Resume() {
        Time.timeScale = 1;
    }

    public static void Endgame() {
        instance.EndGame.SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }

    public void OnEnable() {
        InputManager.AddAction(InputManager.InputEvent.OpenMenu, OpenMenu);
    }
    public void OnDisable() {
        InputManager.RemoveAction(InputManager.InputEvent.OpenMenu, OpenMenu);
    }

    void OpenMenu() {
        Menu.UnFade();
        Time.timeScale = 0;
    }

    
}

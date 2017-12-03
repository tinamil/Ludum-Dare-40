using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public FadeOut Menu;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
	}
	
    public void StartGame() {
        Time.timeScale = 1;
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

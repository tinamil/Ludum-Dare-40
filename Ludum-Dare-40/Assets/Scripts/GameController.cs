using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController instance;

    public FadeOut Menu;
    public GameObject EndGame;
    public TMPro.TextMeshProUGUI VictoryScreen;

    private List<GameObject> stars;
    private float enemiesDestroyed = 0;
    private float playerHit = 0;

    private void Awake() {
        instance = this;
    }

    void Start() {
        stars = new List<GameObject>(GameObject.FindGameObjectsWithTag("Star"));
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

    public static void PickupStar(GameObject gameObject) {
        if (instance.stars.Remove(gameObject))
        {
            EnemySpawner.IncreaseDifficulty();
            if (instance.stars.Count == 0)
            {
                instance.DisplayVictory();
            }
        }
    }

    private void DisplayVictory() {
        string victoryMessage = string.Format(
@"Congratulations on your victory!

You destroyed {0} enemies 
and finished the run in {1} seconds.

Press any key to restart
", enemiesDestroyed, Time.time);

        VictoryScreen.text = victoryMessage;
        VictoryScreen.gameObject.SetActive(true);
    }

    public static void DestroyEnemy(Enemy enemy) {
        instance.enemiesDestroyed += 1;
    }

    internal static void PlayerHit() {
        instance.playerHit += 1;
    }
}

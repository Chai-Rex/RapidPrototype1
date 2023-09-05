using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour {

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text pressSpaceText;
    [SerializeField] private TMP_Text powerText;

    private void Start() {
        scoreText.text = "SCORE " + ScoreManager.Instance.currentScore;
        livesText.text = "LIVES " + Dome.Instance.GetLives();
        powerText.text = "POWER " + Player.Instance.G / 100;

        ScoreManager.Instance.OnScoreChange += ScoreManager_OnScoreChange;
        Dome.Instance.OnLivesChange += Dome_OnLivesChange;
        GameInput.Instance.OnAction += GameInput_OnAction;
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;
        GameInput.Instance.OnIncrease += GameInput_OnIncrease; ;
        GameInput.Instance.OnDecrease += GameInput_OnDecrease; ;

        pressSpaceText.gameObject.SetActive(false);
    }

    private void GameStateManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameStateManager.Instance.IsGamePlaying()) {
            pressSpaceText.gameObject.SetActive(true);
        }
    }

    private void GameInput_OnAction(object sender, System.EventArgs e) {
        if (GameStateManager.Instance.IsGamePlaying()) {
            pressSpaceText.gameObject.SetActive(false);
        }
    }

    private void ScoreManager_OnScoreChange(object sender, System.EventArgs e) {
        scoreText.text = "SCORE " + ScoreManager.Instance.currentScore;

    }

    private void Dome_OnLivesChange(object sender, System.EventArgs e) {
        livesText.text = "LIVES " + Dome.Instance.GetLives();
    }

    private void GameInput_OnIncrease(object sender, System.EventArgs e) {
        if (Player.Instance.G <= Player.Instance.maxG) {
            powerText.text = "POWER " + Player.Instance.G / 100;
        }
    }
    private void GameInput_OnDecrease(object sender, System.EventArgs e) {
        if (Player.Instance.G >= Player.Instance.minG) {
            powerText.text = "POWER " + Player.Instance.G / 100;
        }
    }
}

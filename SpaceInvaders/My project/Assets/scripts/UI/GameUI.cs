using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour {

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text pressSpaceText;

    private void Start() {
        scoreText.text = "SCORE " + ScoreManager.Instance.GetScore();
        livesText.text = "LIVES " + Dome.Instance.GetLives();

        ScoreManager.Instance.OnScoreChange += ScoreManager_OnScoreChange;
        Dome.Instance.OnLivesChange += Dome_OnLivesChange;
        GameInput.Instance.OnAction += GameInput_OnAction;
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;

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
        scoreText.text = "SCORE " + ScoreManager.Instance.GetScore();

    }

    private void Dome_OnLivesChange(object sender, System.EventArgs e) {
        livesText.text = "LIVES " + Dome.Instance.GetLives();
    }
}

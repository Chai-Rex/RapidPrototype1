using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text pressSpaceText;
    [SerializeField] private TMP_Text powerText;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider powerSlider;


    private void Start() {
        scoreText.text =  "" + ScoreManager.Instance.currentScore;
        livesText.text = "LIVES " + Dome.Instance.GetLives();
        powerText.text = "POWER " + Player.Instance.G / 100;
        powerSlider.value = Player.Instance.GetNormalizedPower();
        healthSlider.value = Dome.Instance.GetNormalizedLives();

        ScoreManager.Instance.OnScoreChange += ScoreManager_OnScoreChange;
        Dome.Instance.OnLivesChange += Dome_OnLivesChange;
        GameInput.Instance.OnAction += GameInput_OnAction;
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;
        Player.Instance.OnGChanged += Player_OnGChanged; ; 

        pressSpaceText.gameObject.SetActive(false);
    }

    private void Player_OnGChanged(object sender, System.EventArgs e) {
        powerText.text = "POWER " + Player.Instance.GetNormalizedPower() * 100;
        powerSlider.value = Player.Instance.GetNormalizedPower();
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
        scoreText.text = "" + ScoreManager.Instance.currentScore;

    }

    private void Dome_OnLivesChange(object sender, System.EventArgs e) {
        livesText.text = "LIVES " + Dome.Instance.GetLives();
        healthSlider.value = Dome.Instance.GetNormalizedLives();
    }
}

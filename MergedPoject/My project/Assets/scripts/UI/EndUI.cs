using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EndUI : MonoBehaviour {
    public static EndUI Instance { get; private set; }

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text projectilesBouncedText;
    [SerializeField] private TMP_Text invadersDestroyedText;
    [SerializeField] private TMP_Text specialInvadersDestroyedText;
    [SerializeField] private TMP_Text moonBouncesText;


    private void Awake() {
        Instance = this;

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        this.gameObject.SetActive(false);

        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;
    }

    private void GameStateManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameStateManager.Instance.IsGameOver()) {
            finalScoreText.text = "" + ScoreManager.Instance.currentScore;
            projectilesBouncedText.text = "" + ScoreManager.Instance.projectilesBounced;
            invadersDestroyedText.text = "" + ScoreManager.Instance.invadersDestroyed;
            specialInvadersDestroyedText.text = "" + ScoreManager.Instance.specialInvadersDestroyed;
            moonBouncesText.text = "" + ScoreManager.Instance.moonBounces;
        }
    }



}

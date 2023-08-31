using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EndUI : MonoBehaviour {
    public static EndUI Instance { get; private set; }

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text finalScoreText;


    private void Awake() {
        Instance = this;

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        this.gameObject.SetActive(false);
    }

    public void UpdateFinalScore() {
        finalScoreText.text = "" + ScoreManager.Instance.GetScore();
    }

}

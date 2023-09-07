using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour {

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private Button backButton;
    private void Awake() {

        backButton.onClick.AddListener(() => {
            mainMenuUI.SetActive(true);
            this.gameObject.SetActive(false);
        });


        Time.timeScale = 1f;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button manuelButton;

    [SerializeField] private GameObject infoUI;

    private void Awake() {
        startButton.Select();
        //lambda
        startButton.onClick.AddListener(() => {
            //click
            Loader.Load(Loader.Scene.GameScene);
        });

        quitButton.onClick.AddListener(() => {
            //click
            Application.Quit();
        });

        manuelButton.onClick.AddListener(() => {
            //click
            infoUI.SetActive(true);
            this.gameObject.SetActive(false);
        });

        Time.timeScale = 1f;
    }
}

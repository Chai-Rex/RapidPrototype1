using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
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

        Time.timeScale = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PauseUI : MonoBehaviour {
    public static PauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Slider musicSlider;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    //controls
    [Header("KEYBOARD")]
    [Header("Buttons")]
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button actionButton;
    [SerializeField] private Button pauseButton;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI increaseText;
    [SerializeField] private TextMeshProUGUI decreaseText;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI pauseText;


    private void Awake() {
        Instance = this;

        // pause
        resumeButton.onClick.AddListener(() => {
            GameStateManager.Instance.TogglePauseGame();
        });
        // main menu
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        // sliders
        soundEffectsSlider.onValueChanged.AddListener((float sliderValue) => {
            SoundManager.Instance.ChangeVolume(sliderValue);
            soundEffectsText.text = "Sound Effects: " + Mathf.Ceil(sliderValue * 100);
        });
        musicSlider.onValueChanged.AddListener((float sliderValue) => {
            MusicManager.Instance.ChangeVolume(sliderValue);
            musicText.text = "Music: " + Mathf.Ceil(sliderValue * 100);
        });

        //keyboard
        moveLeftButton.onClick.AddListener(() => { moveLeftText.text = "< >"; GameInput.Instance.RebindBinding(GameInput.Binding.Move_Left, () => { UpdateInputVisuals(); }); });
        moveRightButton.onClick.AddListener(() => { moveRightText.text = "< >"; GameInput.Instance.RebindBinding(GameInput.Binding.Move_Right, () => { UpdateInputVisuals(); }); });
        increaseButton.onClick.AddListener(() => { increaseText.text = "< >"; GameInput.Instance.RebindBinding(GameInput.Binding.Increase, () => { UpdateInputVisuals(); }); });
        decreaseButton.onClick.AddListener(() => { decreaseText.text = "< >"; GameInput.Instance.RebindBinding(GameInput.Binding.Decrease, () => { UpdateInputVisuals(); }); });
        actionButton.onClick.AddListener(() => { actionText.text = "< >"; GameInput.Instance.RebindBinding(GameInput.Binding.Action, () => { UpdateInputVisuals(); }); });
        pauseButton.onClick.AddListener(() => { pauseText.text = "< >"; GameInput.Instance.RebindBinding(GameInput.Binding.Pause, () => { UpdateInputVisuals(); }); });
    }

    private void Start() {
        UpdateInputVisuals();

        GameStateManager.Instance.OnGamePaused += GameBranch_OnGamePaused;
        GameStateManager.Instance.OnGameUnpaused += GameBranch_OnGameUnpaused;

        soundEffectsSlider.value = SoundManager.Instance.GetVolume();
        musicSlider.value = MusicManager.Instance.GetVolume();

        Hide();
    }

    private void GameBranch_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void GameBranch_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    public void Show() {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateInputVisuals() {
        //keyboard
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        increaseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Increase);
        decreaseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Decrease);
        actionText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Action);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }
}

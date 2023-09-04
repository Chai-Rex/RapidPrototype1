using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public enum Binding {
        // keyboard
        Move_Left,
        Move_Right,
        Increase,
        Decrease,
        Action,
        Pause,
    }

    private PlayerInput playerInput;

    public event EventHandler OnMoveLeftStarted;
    public event EventHandler OnMoveLeftCanceled;
    public event EventHandler OnMoveRightStarted;
    public event EventHandler OnMoveRightCanceled;

    public event EventHandler OnIncrease;
    public event EventHandler OnDecrease;

    public event EventHandler OnAction;
    public event EventHandler OnPauseAction;

    private void Awake() {
        Instance = this;

        playerInput = new PlayerInput(); ;

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)) {
            playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }


        playerInput.Player.Enable();

        playerInput.Player.MoveLeft.started += MoveLeft_started;
        playerInput.Player.MoveLeft.canceled += MoveLeft_canceled;
        playerInput.Player.MoveRight.started += MoveRight_started;
        playerInput.Player.MoveRight.canceled += MoveRight_canceled;

        playerInput.Player.Increase.performed += Increase_performed;
        playerInput.Player.Decrease.performed += Decrease_performed;

        playerInput.Player.Action.performed += Action_performed;
        playerInput.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        playerInput.Player.MoveLeft.started -= MoveLeft_started;
        playerInput.Player.MoveLeft.canceled -= MoveLeft_canceled;
        playerInput.Player.MoveRight.started -= MoveRight_started;
        playerInput.Player.MoveRight.canceled -= MoveRight_canceled;

        playerInput.Player.Action.performed -= Action_performed;
        playerInput.Player.Pause.performed -= Pause_performed;

        playerInput.Dispose();
    }

    private void MoveLeft_started(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnMoveLeftStarted?.Invoke(this, EventArgs.Empty);
    }
    private void MoveLeft_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnMoveLeftCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void MoveRight_started(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnMoveRightStarted?.Invoke(this, EventArgs.Empty);
    }
    private void MoveRight_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnMoveRightCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void Increase_performed(InputAction.CallbackContext obj) {
        OnIncrease?.Invoke(this, EventArgs.Empty);
    }
    private void Decrease_performed(InputAction.CallbackContext obj) {
        OnDecrease?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }


    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnAction?.Invoke(this, EventArgs.Empty);
    }


    public string GetBindingText(Binding binding) {
        switch (binding) {
            // KEYBOARD
            case Binding.Move_Left:
                return playerInput.Player.MoveLeft.bindings[0].ToDisplayString();

            case Binding.Move_Right:
                return playerInput.Player.MoveRight.bindings[0].ToDisplayString();

            case Binding.Increase:
                return playerInput.Player.Increase.bindings[1].ToDisplayString();

            case Binding.Decrease:
                return playerInput.Player.Decrease.bindings[1].ToDisplayString();

            case Binding.Action:
                return playerInput.Player.Action.bindings[0].ToDisplayString();

            case Binding.Pause:
                return playerInput.Player.Pause.bindings[0].ToDisplayString();

            default:
                return null;
        }
    }


    public void RebindBinding(Binding binding, Action onActionRebound) {

        playerInput.Player.Disable();

        InputAction inputAction;
        int bindingIndex;


        switch (binding) {
            // KEYBOARD
            case Binding.Move_Left:
                inputAction = playerInput.Player.MoveLeft;
                bindingIndex = 0;

                break;
            case Binding.Move_Right:
                inputAction = playerInput.Player.MoveRight;
                bindingIndex = 0;

                break;
            case Binding.Increase:
                inputAction = playerInput.Player.Increase;
                bindingIndex = 1;

                break;
            case Binding.Decrease:
                inputAction = playerInput.Player.Decrease;
                bindingIndex = 1;

                break;
            case Binding.Action:
                inputAction = playerInput.Player.Action;
                bindingIndex = 0;

                break;
            case Binding.Pause:
                inputAction = playerInput.Player.Pause;
                bindingIndex = 0;

                break;
            default:
                Debug.LogError("passing invalid Binding into RebindBiding");
                return;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                //callback.action.bindings[1].path;
                callback.Dispose();
                playerInput.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInput.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

            })
            .Start();

    }

}

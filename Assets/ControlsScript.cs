using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsScript : MonoBehaviour
{
    public static ControlsScript Instance { get; private set; }

    public PlayerInput _playerInput;
    public InputActionAsset _playerInputAsset;
    public InputActionMap _playerInputMap;

    //Input actions
    public InputAction _placeMoreTowers;
    public InputAction _toggleSettings;

    //Inputs
    public bool placeMoreTowersHeld { get; private set; }
    public bool toggleSettingsPressed { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        _playerInput = GetComponent<PlayerInput>();
        _playerInputMap = _playerInputAsset.FindActionMap("Player");
        SetupInputActions();
    }

    void Update()
    {
        UpdateInputActions();
    }

    private void SetupInputActions()
    {
        _placeMoreTowers = _playerInput.actions["PlaceMoreTowers"];
        _toggleSettings = _playerInput.actions["ToggleSettings"];
    }

    private void UpdateInputActions()
    {
        placeMoreTowersHeld = _placeMoreTowers.IsPressed();
        toggleSettingsPressed = _toggleSettings.WasPressedThisFrame();
    }
}

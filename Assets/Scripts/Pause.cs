using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private GameObject pauseMenu;

    InputAction toggleMenu;

    private void Start()
    {
        toggleMenu = actionAsset.FindActionMap("XRI LeftHand Interaction").FindAction("Menu");
        toggleMenu.Enable();
        toggleMenu.performed += TogglePauseMenu;
    }

    private void TogglePauseMenu(InputAction.CallbackContext context)
    {
        toggleMenu.Disable();
        pauseMenu.transform.position = gameObject.transform.position;
        pauseMenu.transform.rotation = gameObject.transform.rotation;
        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.transform.position = new Vector3(0f, 0f, 0f);
        pauseMenu.transform.rotation = Quaternion.identity;
        Time.timeScale = 1.0f;
        toggleMenu.Enable();
    }
}

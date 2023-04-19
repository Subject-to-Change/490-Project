using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public GameObject canvas;

    private GameObject return_button;
    private PrimaryPlayerController movement_controller;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
        return_button = GameObject.Find("ReturnGame");
        movement_controller = GameObject.Find("Hero").GetComponent<PrimaryPlayerController>();
        canvas.SetActive(false);
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!canvas.activeInHierarchy)
            {
                PauseGame();
            }
            else
            {
                ReturnToGame();
            }

        }
    }

    private void PauseGame()
    {
        movement_controller.enabled = false;
        canvas.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(return_button);
    }

    /// <summary>
    /// Returns to game.
    /// </summary>
    public void ReturnToGame()
    {
        movement_controller.enabled = true;
        canvas.SetActive(false);
        Time.timeScale = 1f;
        EventSystem.current.SetSelectedGameObject(null);
    }
}

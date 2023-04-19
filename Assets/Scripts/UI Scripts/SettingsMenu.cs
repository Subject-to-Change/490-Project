using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public GameObject canvas;
    public GameObject return_game_button, return_menu_button, music_slider, sfx_slider;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
                Time.timeScale = 1f;
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                canvas.SetActive(true);
                Time.timeScale = 0f;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(return_game_button);
            }

        }
    }

    /// <summary>
    /// Returns to game.
    /// </summary>
    public void ReturnToGame()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }
}

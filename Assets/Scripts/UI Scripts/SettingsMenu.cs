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

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
        return_button = GameObject.Find("ReturnGame");
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
        canvas.SetActive(false);
        Time.timeScale = 1f;
        EventSystem.current.SetSelectedGameObject(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject canvas;

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
            canvas.SetActive(true);
            Time.timeScale = 0f;
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

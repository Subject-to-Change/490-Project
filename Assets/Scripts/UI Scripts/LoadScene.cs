using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public float fade_duration = 1f;
    public Image fade_image;

    // Start is called before the first frame update
    void Start()
    {
        fade_image.color = new Color(0f, 0f, 0f, 1f);
        fade_in();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Calls coroutine for fading in. This is used for scene
    /// transitions and time warping.
    /// </summary>
    public void fade_in()
    {
        StartCoroutine(FadeInCoroutine());
    }

    /// <summary>
    /// Calls coroutine for fading out. This is used for time
    /// warping
    /// </summary>
    public void fade_out()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    /// <summary>
    /// Coroutine that performs the fade in from black.
    /// </summary>
    IEnumerator FadeInCoroutine()
    {
        float timer = 0f;

        while (timer < fade_duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fade_duration);
            fade_image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Calls coroutine for fading out of scene and transitioning between
    /// scenes.
    /// </summary>
    public void fade_out_scene(string scene_name)
    {
        StartCoroutine(FadeOutTransitionCoroutine(scene_name));
    }

    /// <summary>
    /// Coroutine that performs the fade out and
    /// scene transition
    /// </summary>
    IEnumerator FadeOutTransitionCoroutine(string scene_name)
    {
        float timer = 0f;

        while (timer < fade_duration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fade_duration);
            fade_image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(scene_name);
    }

    /// <summary>
    /// Coroutine that performs only a fade to black
    /// </summary>
    IEnumerator FadeOutCoroutine()
    {
        float timer = 0f;

        while (timer < fade_duration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fade_duration);
            fade_image.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        Time.timeScale = 1f;
    }

    public void LoadNewScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }

    public void ResetHealth()
    {
        HealthManager.ResetHealthStatic();
    }
}

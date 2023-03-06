using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1.0f);
        }
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 1.0f);
        }

        if (gameObject.name == "SFXSlider")
        {
            slider.value = PlayerPrefs.GetFloat("volume");
        }
        if (gameObject.name == "MusicSlider")
        {
            slider.value = PlayerPrefs.GetFloat("music");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusic()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
    }

    public void SetSFX()
    {
        PlayerPrefs.SetFloat("music", slider.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController: MonoBehaviour
{
    public Slider music_slider;
    public Slider sfx_slider;
    public AudioMixer master_mixer;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1.0f);
        }
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 1.0f);
        }
        
        music_slider.value = PlayerPrefs.GetFloat("volume");
        sfx_slider.value = PlayerPrefs.GetFloat("music");
        
        music_slider.onValueChanged.AddListener(SetMusic);
        sfx_slider.onValueChanged.AddListener(SetSFX);

        SetMusic(music_slider.value);
        SetSFX(sfx_slider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMusic(float value)
    {
        PlayerPrefs.SetFloat("music", value);
        master_mixer.SetFloat("Music", Mathf.Log10(value) * 20);
    }

    public void SetSFX(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
        master_mixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }
}

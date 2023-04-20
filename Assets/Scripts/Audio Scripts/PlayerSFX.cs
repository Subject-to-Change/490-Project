using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public GameObject player_audio_object;

    private AudioSource player_audio;
    // Start is called before the first frame update
    void Start()
    {
        player_audio = player_audio_object.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Footstep()
    {
        player_audio.Play();
    }
}

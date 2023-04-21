using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundHandler : MonoBehaviour
{

    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;
    public AudioClip landSound;

    private AudioSource jumpLandAudioSource;
    private AudioSource fallingAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        jumpLandAudioSource = gameObject.transform.Find("GenrealOneShotAudioSource").GetComponent<AudioSource>();
        fallingAudioSource = gameObject.transform.Find("FallingSoundAudioSource").GetComponent<AudioSource>();
        Debug.Assert(jumpLandAudioSource);
        Debug.Assert(fallingAudioSource);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playJump() {
        jumpLandAudioSource.PlayOneShot(jumpSound); 
    }

    public void playDoubleJump() {
        jumpLandAudioSource.PlayOneShot(doubleJumpSound); 
    }

    public void playLand() {
        jumpLandAudioSource.PlayOneShot(landSound); 
    }

    public void updateFallVelocity(float x) {
        fallingAudioSource.volume = Mathf.Pow(Mathf.Clamp(x,0,1),2)*0.3f;
    }


}

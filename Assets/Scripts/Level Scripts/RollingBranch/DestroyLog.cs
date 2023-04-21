using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLog : MonoBehaviour
{
    public ParticleSystem explosion;
    public AudioClip crash;
    public AudioClip break_branch;

    private Animator this_anim;
    private AudioSource this_audio_source;

    private void Start()
    {
        Invoke("ShrinkAndBlow", 8f);
        this_anim = this.GetComponent<Animator>();
        this_audio_source = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShrinkAndBlow();
        } else if (other.gameObject.CompareTag("Ground"));
        {
            this_audio_source.PlayOneShot(crash);
        }
    }

    private void ShrinkAndBlow()
    {
        this_anim.SetTrigger("shrink");
        explosion.Emit(10);
        this_audio_source.PlayOneShot(break_branch);
    }

    public void DeleteSelf()
    {
        Destroy(gameObject);
    }
}

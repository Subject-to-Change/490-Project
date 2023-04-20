using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLog : MonoBehaviour
{
    public ParticleSystem explosion;

    private Animator this_anim;

    private void Start()
    {
        Invoke("ShrinkAndBlow", 8f);
        this_anim = this.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShrinkAndBlow();
        }
    }

    private void ShrinkAndBlow()
    {
        this_anim.SetTrigger("shrink");
        explosion.Emit(10);
    }

    public void DeleteSelf()
    {
        Destroy(gameObject);
    }
}

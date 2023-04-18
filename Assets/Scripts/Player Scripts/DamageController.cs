using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{

    private HealthManager health_manager;
    private string instadeath_tag = "Death";
    private string damage_tag = "Damage";
    private bool iframe = false;

    private void Start()
    {
        health_manager = GameObject.Find("HealthSystem").GetComponent<HealthManager>();

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!iframe)
        {
            if (other.gameObject.CompareTag(instadeath_tag))
            {
                health_manager.KillPlayer();
            }
            else if (other.gameObject.CompareTag(damage_tag))
            {

                health_manager.DamagePlayer();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!iframe)
        {
            if (other.gameObject.CompareTag(damage_tag))
            {

                health_manager.DamagePlayer();
            }
        }

    }

    public void ToggleIFrame()
    {
        iframe = !iframe;
    }
}

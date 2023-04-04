using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int iframe_cooldown = 2;

    private GameObject hud_controller;
    private string instadeath_tag = "Death";
    private string damage_tag = "Damage";
    private bool iframe = false;

    private void Start()
    {
        hud_controller = GameObject.Find("HealthAbilitiesSystem");

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!iframe)
        {
            if (other.gameObject.CompareTag(instadeath_tag))
            {
                hud_controller.SendMessage("KillPlayer", iframe_cooldown);
            }
            else if (other.gameObject.CompareTag(damage_tag))
            {

                hud_controller.SendMessage("DamagePlayer", iframe_cooldown);
            }
        }

    }

    public void ToggleIFrame()
    {
        iframe = !iframe;
    }
}

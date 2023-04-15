using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onDeath;
    public static readonly int MAX_LIVES = 3;
    private static int life = MAX_LIVES;
    private float iframe_timer = 2.0f;
    private HUDManager hud_manager;
    private DamageController damage_controller;

    // Start is called before the first frame update
    void Start()
    {
        damage_controller = GameObject.Find("Hero").GetComponent<DamageController>();
        hud_manager = GameObject.Find("HUDSystem").GetComponent<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static int GetHealth()
    {
        return life;
    }

    public void DamagePlayer()
    {
        life--;
        hud_manager.DisplayHearts();
        if (life == 0)
        {
            KillPlayer();
        }
        else
        {
            damage_controller.ToggleIFrame();
            StartCoroutine(IFrameCooldown());
        }


    }

    private IEnumerator IFrameCooldown()
    {
        yield return new WaitForSeconds(iframe_timer);
        damage_controller.ToggleIFrame();
    }

    public void KillPlayer()
    {
        onDeath.Invoke();
        damage_controller.ToggleIFrame();
        StartCoroutine(IFrameCooldown());
    }

    public void ResetHealth()
    {
        life = MAX_LIVES;
        hud_manager.DisplayHearts();
    }

    public static void ResetHealthStatic()
    {
        life = MAX_LIVES;
    }
}

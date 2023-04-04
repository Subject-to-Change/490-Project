using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAbilities : MonoBehaviour
{
    private Icons hud_icons;
    private Abilities hud_abilities;
    private GameObject hero;
    private GameObject scene_transition_controller;

    private static readonly int max_lives = 3;
    private static int life = max_lives;

    private void Start()
    {

        // find hearts and icons
        GameObject hearts_container = GameObject.Find("Hearts");
        Image[] hearts = hearts_container.GetComponentsInChildren<Image>();
        Image hourglass = GameObject.Find("Hourglass").GetComponent<Image>();

        // assign references
        hud_icons = new Icons(hearts, hourglass);
        hud_abilities = SaveLoad.LoadAbilityData();
        hero = GameObject.Find("Hero");
        scene_transition_controller = GameObject.Find("SceneTransitionController");

        // display appropriate hearts
        DisplayHearts();

        // display appropriate abilities
        if (hud_abilities.get_time_warp())
        {
            hud_icons.display_hourglass();
        }
        else
        {
            hud_icons.hide_hourglass();
        }
    }

    private void DisplayHearts()
    {
        for (int i = 0; i < max_lives; i++)
        {
            if (i < life)
            {
                hud_icons.display_heart(i);
            }
            else
            {
                hud_icons.hide_heart(i);
            }
        }
    }

    public void DamagePlayer(int iframe_timer)
    {
        Debug.Log("message received");
        life--;
        if (life == 0)
        {
            GameOver();
        }

        DisplayHearts();
        hero.SendMessage("ToggleIFrame");
        StartCoroutine(IFrameCooldown(iframe_timer));
    }

    private IEnumerator IFrameCooldown(int iframe_timer)
    {
        yield return new WaitForSeconds(iframe_timer);

        hero.SendMessage("ToggleIFrame");

        
    }

    public void KillPlayer()
    {

    }

    private void GameOver()
    {
        scene_transition_controller.SendMessage("fade_out_scene", "Title");
    }

    public static void ResetHealth()
    {
        life = max_lives;
    }

}

public class Icons
{
    Image[] hearts;
    Image hourglass;

    public Icons(Image[] hearts, Image hourglass)
    {
        this.hearts = hearts;
        this.hourglass = hourglass;
    }

    public void display_heart(int index)
    {
        hearts[index].enabled = true;
    }

    public void hide_heart(int index)
    {
        hearts[index].enabled = false;
    }

    public void display_hourglass()
    {
        hourglass.enabled = true;
    }

    public void hide_hourglass()
    {
        hourglass.enabled = false;
    }
}

[System.Serializable]
public class Abilities
{
    private bool time_warp;
    private bool double_jump;
    private bool slide;

    public Abilities(bool time_warp, bool double_jump, bool slide)
    {
        this.time_warp = time_warp;
        this.double_jump = double_jump;
        this.slide = slide;
    }

    public void set_time_warp(bool new_value)
    {
        this.time_warp = new_value;
    }

    public void set_double_jump(bool new_value)
    {
        this.double_jump = new_value;
    }

    public void set_slide(bool new_value)
    {
        this.slide = new_value;
    }

    public bool get_time_warp()
    {
        return this.time_warp;
    }

    public bool get_double_jump()
    {
        return this.double_jump;
    }

    public bool get_slide()
    {
        return this.slide;
    }
}
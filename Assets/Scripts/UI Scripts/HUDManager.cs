using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private Icons hud_icons;
    public Abilities hud_abilities;
    private GameObject hero;
    private GameObject scene_transition_controller;
    private GameObject respawn_marker;

    private void Start()
    {

        // find hearts and icons
        GameObject hearts_container = GameObject.Find("Hearts");
        Image[] hearts = hearts_container.GetComponentsInChildren<Image>();
        Image hourglass = GameObject.Find("Hourglass").GetComponent<Image>();

        // assign references
        hud_icons = new Icons(hearts, hourglass);
        hud_abilities = Abilities.LoadAbilityData();
        hero = GameObject.Find("Hero");
        hud_abilities.connectToPlayer(hero.GetComponent<PrimaryPlayerController>());
        scene_transition_controller = GameObject.Find("SceneTransitionController");
        respawn_marker = GameObject.Find("RespawnMarker");

        // display appropriate hearts
        DisplayHearts();

        // display appropriate abilities
        if (hud_abilities.time_warp)
        {
            hud_icons.display_hourglass();
        }
        else
        {
            hud_icons.hide_hourglass();
        }
    }

    public void DisplayHearts()
    {
        int num_lives = HealthManager.GetHealth();
        for (int i = 0; i < HealthManager.MAX_LIVES; i++)
        {
            if (i < num_lives)
            {
                hud_icons.display_heart(i);
            }
            else
            {
                hud_icons.hide_heart(i);
            }
        }
    }

    public void UpdateAbilitiesDisplay()
    {
        if (hud_abilities.time_warp)
        {
            hud_icons.display_hourglass();
        }
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


    private bool _time_warp;
    private bool _double_jump;
    private bool _slide;
    private bool _glide;

    public bool time_warp;
    [SerializeField]
    public bool double_jump{
        get {return _double_jump;}
        set {
            if(playerController) playerController.multiJumpEnabled = value;
            _double_jump = value;
        }
    }
    [SerializeField]
    public bool slide{
        get {return _slide;}
        set {
            if(playerController) playerController.canSlide = value;
            _slide = value;
        }
    }
    [SerializeField]
    public bool glide{
        get {return _glide;}
        set {
            if(playerController) playerController.glideEnabled = value;
            _glide = value;
        }
    }

    private PrimaryPlayerController playerController; 

    public Abilities()
    {
        this.time_warp = false;
        this.double_jump = false;
        this.slide = false;
    }

    public Abilities(bool time_warp, bool double_jump, bool slide)
    {
        this.time_warp = time_warp;
        this.double_jump = double_jump;
        this.slide = slide;
    }

    public void connectToPlayer(PrimaryPlayerController playerController) {
        this.playerController = playerController;

        //Resave all values so they are set to the player controller
        glide = glide;
        slide = slide;
        double_jump = double_jump;
        //time_warp = time_warp;
    }

    public static void SaveAbilityData(Abilities data)
    {
        SaveLoad.SaveData<Abilities>(data, GetPath());
    }

    public static Abilities LoadAbilityData()
    {
        return SaveLoad.LoadAbilityData<Abilities>(GetPath());
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/abilitydata.json";
    }
}
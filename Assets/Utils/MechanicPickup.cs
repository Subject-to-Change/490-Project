using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicPickup : MonoBehaviour
{

    public enum action {
        enableDoubleJump,
        enableTimeWarp,
        enableGlide,
        enableSlide
    }

    public action unlock = action.enableDoubleJump;

    HUDManager hud;
    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.Find("HUDSystem")?.GetComponent<HUDManager>();
        if(!hud) Debug.LogError("MechanicPickup could not find hud manager, looking for \"HUDSystem\" gameobject.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        switch(unlock) {
            case action.enableDoubleJump:
                hud.hud_abilities.double_jump = true;
                break;
            case action.enableGlide:
                hud.hud_abilities.glide = true;
                break;
            case action.enableSlide:
                hud.hud_abilities.slide = true;
                break;
            case action.enableTimeWarp:
                hud.hud_abilities.time_warp = true;
                break;
        }
        hud.UpdateAbilitiesDisplay();
        gameObject.SetActive(false);
    }
}

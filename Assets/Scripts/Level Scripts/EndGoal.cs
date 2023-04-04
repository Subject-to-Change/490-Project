using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    public string next_scene_name;

    private GameObject scene_transition_controller;
    
    void Start()
    {
        scene_transition_controller = GameObject.Find("SceneTransitionController");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            scene_transition_controller.SendMessage("fade_out_scene", next_scene_name);
        }
    }

}

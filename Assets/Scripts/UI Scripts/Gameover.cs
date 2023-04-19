using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gameover : MonoBehaviour
{
    private LoadScene scene_loader;
    private Animator appear;
    private GameObject gameover_button;

    // Start is called before the first frame update
    void Start()
    {
        scene_loader = GameObject.Find("SceneTransitionController").GetComponent<LoadScene>();
        appear = GameObject.Find("Gameover Screen").GetComponent<Animator>();
        gameover_button = GameObject.Find("Gameover Button");
    }

    public void end_game()
    {
        scene_loader.fade_out();
        Invoke("show_text", 1f);
    }

    private void show_text()
    {
        appear.SetTrigger("show");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gameover_button);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpActivator : MonoBehaviour
{
    public int activated;
    public Vector3 newPosition;
    public GameObject player;
    public LoadScene fadeToBlack;
    public float fadeToBlackTimer;

    private HUDManager hud;
    // Start is called before the first frame update
    void Start()
    {
        activated = 0;
        player = GameObject.Find("Hero");
        fadeToBlack = GameObject.Find("SceneTransitionController").GetComponent<LoadScene>();
        fadeToBlackTimer = fadeToBlack.fade_duration;
        hud = GameObject.Find("HUDSystem").GetComponent<HUDManager>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public IEnumerator waiter()
    {

        yield return new WaitForSeconds(fadeToBlackTimer);
        player.transform.position = newPosition;
        fadeToBlack.fade_in();
    }

    public void teleportPlayer()
    {
        if (hud.hud_abilities.time_warp)
        {
            fadeToBlack.fade_out();
        StartCoroutine(waiter());
        }
    }


}

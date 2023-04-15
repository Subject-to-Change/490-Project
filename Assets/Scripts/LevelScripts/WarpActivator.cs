using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpActivator : MonoBehaviour
{
    public int activated;
    public Vector3 newPosition;
    public GameObject player;
    public LoadScene fadeToBlack;
    public int fadeToBlackTime;
    // Start is called before the first frame update
    void Start()
    {
        activated = 0;
        player = GameObject.Find("Hero");
        fadeToBlack = GameObject.Find("SceneTransitionController").GetComponent<LoadScene>();
        fadeToBlack.fade_duration = fadeToBlackTime;
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void teleportPlayer()
    {
        fadeToBlack.fade_in();
            player.transform.position = newPosition;
        fadeToBlack.fade_out();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpActivator : MonoBehaviour
{
    public int activated;
    Vector3 newPosition;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        activated = 0;
        player = GameObject.Find("Hero");
        newPosition.x = 0f;
        newPosition.y = 0f;
        newPosition.z = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //todo: replacer with global variable name
        if(activated == 1 && Input.GetKeyDown("w"))
        {

            player.transform.position = newPosition;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        activated = 1;

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        activated = 0;

    }

}

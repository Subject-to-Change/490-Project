using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{ private int activated = 0;
    public float smoothTime;
    public float descentSmoothTime;
    public float speed;
    public float descentspeed;
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 previous;
    public float velocity = 0;
    public float velocity2 = 0;
    public int destination = 0;
    public string destName;
    public string startName;
    public string elevatorName;
    public GameObject player;
    // 0 means it needs to ascend, 1 means it needs to descend
    // Start is called before the first frame update
    void Start()
    {

    player = player = GameObject.FindWithTag("Player");
    smoothTime = .7f;
    speed = 5f;
    descentspeed = 3;
    //grab position of parent object
    startPosition = GameObject.Find(startName).transform.position;
     
    //grab position of destination 
    endPosition = GameObject.Find(destName).transform.position;

        player = GameObject.Find("Hero");

    velocity = speed;

    velocity2 = descentspeed;

    descentSmoothTime = 1f;

    }

    // Called roughly every .02 seconds
    void FixedUpdate()
    {

        if (activated == 1 && destination == 0)
        {
            float goal = Mathf.SmoothDamp(transform.position.y, endPosition.y, ref velocity, smoothTime);
            transform.position = new Vector3(transform.position.x, goal, transform.position.z);
            if(Math.Abs(endPosition.y - transform.position.y) < .01)
            {

                this.destination = 1;
                velocity2 = descentspeed;
            }
        }
        else if (activated == 1 && destination == 1)
        {

            float goal2 = Mathf.SmoothDamp(transform.position.y, startPosition.y, ref velocity2, descentSmoothTime);


            transform.position = new Vector3(transform.position.x, goal2, transform.position.z);
            if (Math.Abs(transform.position.y - startPosition.y) < .01)
            {
                this.destination = 0;
                velocity = speed;
            }

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.transform.SetParent(GameObject.Find(elevatorName).transform);
        activated = 1;

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.transform.SetParent(null);

    }

}

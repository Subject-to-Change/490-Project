using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform destination;
    public Transform origin;
    public float travel_time = 5f;
    public float wait_time = 2f;

    private float speed;
    private Vector3 next_pos;
    private bool moving = true;
    public GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        next_pos = destination.position;
        speed = Vector3.Distance(origin.position, destination.position) / travel_time;
        hero = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, next_pos, speed * Time.deltaTime);

            if (transform.position == next_pos)
            {
                moving = false;
                Invoke("ChangeDirection", wait_time);
            }
        }
    }

    private void ChangeDirection()
    {
        moving = true;
        if (next_pos == origin.position)
        {
            next_pos = destination.position;
        }
        else
        {
            next_pos = origin.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        hero.transform.SetParent(transform);

    }

    private void OnCollisionExit2D(Collision2D other)
    {

        hero.transform.SetParent(null);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRollingBall : MonoBehaviour
{
    public GameObject ball;

    private Rigidbody2D ball_rb;
    // Start is called before the first frame update
    void Start()
    {
        ball_rb = ball.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Wakes up the ball so it can begin to roll.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            Debug.Log("new object");
            ball_rb.WakeUp();
        }
    }
}

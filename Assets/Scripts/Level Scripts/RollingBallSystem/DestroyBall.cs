using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Destroys the ball when it hits the trigger
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            Destroy(other.gameObject);
        }
    }
}

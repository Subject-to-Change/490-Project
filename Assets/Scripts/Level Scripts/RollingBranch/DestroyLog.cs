using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "DestroyLog")
        {
            Destroy(gameObject);
        }
    }
}

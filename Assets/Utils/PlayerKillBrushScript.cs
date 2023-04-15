using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillBrushScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            other.GetComponent<PlayerHealth>().damage(100000);  //Ensure damage is enough to kill player at any health
        }
    }
}

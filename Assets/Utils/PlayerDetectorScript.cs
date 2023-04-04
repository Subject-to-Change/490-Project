using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorScript : MonoBehaviour
{
    private Collider2D collisionBox;
    private GameObject player;
    private Collider2D playerCollider;
    private bool previousState = false;

    // Start is called before the first frame update
    void Start()
    {
        collisionBox = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) {
            gameObject.SetActive(false);
        }
        Debug.Log(player.name);
        playerCollider = player.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(previousState != playerInBounds()) {
            previousState = !previousState;
            if(previousState)
                onPlayerEnter();
            else
                onPlayerExit();
        }
    }

    bool playerInBounds() {
        return collisionBox.IsTouching(playerCollider);
    }

    void onPlayerEnter() {
        Debug.Log("Enter");
    }

    void onPlayerExit() {
        Debug.Log("Exit");
    }
}

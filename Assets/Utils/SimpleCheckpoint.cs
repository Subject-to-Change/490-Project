using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCheckpoint : MonoBehaviour
{

    GameObject respawnPreview;  //Transform for this checkpoint's respawn locvation
    GameObject respawnMarker;  //Transform for respawn for the entire scene
    private Collider2D collisionBox;
    private GameObject player;
    private Collider2D playerCollider;
    private bool previousState = false;

    // Start is called before the first frame update
    void Start()
    {
        respawnMarker = GameObject.Find("RespawnMarker");
        respawnPreview = this.gameObject.transform.Find("RespawnPreview").gameObject;

        collisionBox = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) {
            gameObject.SetActive(false);
        }
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
        respawnMarker.transform.position = respawnPreview.transform.position;
    }

    void onPlayerExit() {
    }
}

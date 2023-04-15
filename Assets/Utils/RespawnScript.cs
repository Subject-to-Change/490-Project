using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{

    private GameObject player;
    //private PlayerHealth playerHealth;
    private HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) 
            Debug.LogError("RespawnScript could not find player GameObject, looking for \"Player\" tag.");
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //playerHealth.onDeath.AddListener(startRespawnCoroutine);
        healthManager = GameObject.Find("HealthSystem").GetComponent<HealthManager>();
        healthManager.onDeath.AddListener(startRespawnCoroutine);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startRespawnCoroutine() {
        StartCoroutine(respawnCoroutine());
    }

    private IEnumerator respawnCoroutine() {
        //First, prevent the player from moving
        RigidbodyConstraints2D playerConstraints = player.GetComponent<Rigidbody2D>().constraints;
        RigidbodyConstraints2D playerConstraintsLocked = playerConstraints | RigidbodyConstraints2D.FreezePosition;
        player.GetComponent<Rigidbody2D>().constraints = playerConstraintsLocked;

        //Hide the player object
        player.transform.Find("characterRootNode").gameObject.SetActive(false);

        //Wait 0.2 seconds
        yield return new WaitForSeconds(0.2f);

        //Teleport player to respawn marker
        player.transform.position = this.transform.position;

        //Wait 0.2 seconds
        yield return new WaitForSeconds(0.2f);

        //Enable player movement
        player.GetComponent<Rigidbody2D>().constraints = playerConstraints;

        //Show the player object
        player.transform.Find("characterRootNode").gameObject.SetActive(true);

        //Heal player to max health (Now handled elsewhere with UI code)
        healthManager.ResetHealth();

        //Stop coroutine
        yield break;
    }
}

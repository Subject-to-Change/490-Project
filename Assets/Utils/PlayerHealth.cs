using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    int maxHealth = 100;
    int currentHealth = 100;
    public UnityEngine.Events.UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(int amount) {
        if(currentHealth<=0)
            return;
        if(currentHealth-amount <= 0) {
            //Player dies, invoke event
            currentHealth = 0;
            onDeath.Invoke();
        } else {
            currentHealth -= amount;
        }
    }

    public void healToMax() {
        currentHealth = maxHealth;
    }

    
}

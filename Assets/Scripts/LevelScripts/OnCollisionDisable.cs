using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDisable : MonoBehaviour
{
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        Destroy(target);

    }

}

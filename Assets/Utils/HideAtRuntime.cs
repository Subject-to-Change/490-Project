using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAtRuntime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}

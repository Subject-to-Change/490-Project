using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{

    public GameObject controller;
    public int piece = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPiece()
    {

        FindObjectOfType<PuzzleControllerScript>().addOrder(piece);
        Debug.Log("setPiece reached");
    }

    

}

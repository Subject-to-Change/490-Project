using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleControllerScript : MonoBehaviour
{

    int[] order;
    int[] solution;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        order = new int[3];
        solution = new int[3];
        solution[0] = 1;
        solution[1] = 2;
        solution[2] = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (solution.Equals(order))
       {

            //call event to open door
            Debug.Log("Solution reached");
        }

         else if (order[2] != 0)
        {   

            Array.Clear(order, 0, order.Length);
            i = 0;
        }

    }

    public void addOrder(int piece)
    {

        order[i] = piece;
        i++;
    }


}

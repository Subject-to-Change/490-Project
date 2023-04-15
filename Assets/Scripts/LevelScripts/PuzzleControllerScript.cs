using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PuzzleControllerScript : MonoBehaviour
{

    int[] order;
    int[] solution;
    int i;
    bool solved;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        order = new int[4];
        solution = new int[4];
        solution[0] = 1;
        solution[1] = 3;
        solution[2] = 4;
        solution[3] = 2;
    }

    // Update is called once per frame
    void Update()
    {

        if (solution.SequenceEqual(order))
        {

            Debug.Log("Solution reached");

        }
         else if (order[3] != 0)
        {

            Array.Clear(order, 0, order.Length);
            i = 0;
            Debug.Log("Clear reached");
        }

    }

    public void addOrder(int piece)
    {
        Debug.Log("addOrder reached");
        order[i] = piece;
        i++;
    }


}

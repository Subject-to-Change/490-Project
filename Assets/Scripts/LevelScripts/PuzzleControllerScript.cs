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
    public GameObject objectActive;
    public GameObject objectActive2;
    public GameObject objectActive3;
    public GameObject objectActive4;
    public AudioClip puzzleFail;
    public AudioClip puzzleFinish;
    private AudioSource audioSource;

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
        audioSource = GameObject.Find("PuzzleSound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (solution.SequenceEqual(order))
        {

            Debug.Log("Solution reached");
            //add finish code before clearing array
            if (!objectActive.activeSelf)
            {
                objectActive.SetActive(true);
                objectActive2.SetActive(true);
                objectActive3.SetActive(true);
                objectActive4.SetActive(true);
                audioSource.PlayOneShot(puzzleFinish);
            }
            Array.Clear(order,0,order.Length);
            i = 0;

        }
         else if (order[3] != 0)
        {

            Array.Clear(order, 0, order.Length);
            audioSource.PlayOneShot(puzzleFail);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// This closes the application when called.
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Application Exits");
        Application.Quit();
    }
}

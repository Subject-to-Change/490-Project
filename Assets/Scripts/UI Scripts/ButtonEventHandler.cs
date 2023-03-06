using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEventHandler : MonoBehaviour
{
    public GameObject next_menu;

    private Animator parent_animator;
    private Animator next_animator;
    private bool cooldown = false;

    /// <summary>
    /// On awake, gets parent animator and next menu's animator.
    /// </summary>
    void Awake()
    {
        parent_animator = gameObject.GetComponentInParent<Animator>();
        next_animator = next_menu.GetComponent<Animator>();
    }

    /// <summary>
    /// Shows the first menu, i.e., the title screen
    /// </summary>
    void OnEnable()
    {
        if (gameObject.CompareTag("Start Menu"))
        {
            parent_animator.SetTrigger("show");
        }
    }

    /// <summary>
    /// This closes the application when called.
    /// </summary>
    public void ExitGame()
    {
        Debug.Log("Application Exits");
        Application.Quit();
    }

    /// <summary>
    /// Makes the menu slide to the left and disappear and make the next
    /// menu appear in its place.
    /// </summary>
    public void MenuTransition()
    {
        if (!cooldown)
        {
            parent_animator.SetTrigger("show");
            StartCoroutine("NextMenuAppear");
            cooldown = true;
        }

    }

    /// <summary>
    /// Handles the transition to the next menu.
    /// </summary>
    private IEnumerator NextMenuAppear()
    {
        yield return new WaitForSeconds(1.0f);
        next_animator.SetTrigger("show");
        cooldown = false;
    }


}

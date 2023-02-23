using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventHandler : MonoBehaviour
{
    public GameObject next_menu;

    private Animator parent_animator;
    private Animator next_animator;
    private bool cooldown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        parent_animator = gameObject.GetComponentInParent<Animator>();
        next_animator = next_menu.GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (gameObject.name == "Play")
        {
            parent_animator.SetTrigger("show");
        }
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

    /// <summary>
    /// makes the menu slide to the left and disappear and make the next
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

    private IEnumerator NextMenuAppear()
    {
        yield return new WaitForSeconds(1.0f);
        next_animator.SetTrigger("show");
        cooldown = false;
    }
}

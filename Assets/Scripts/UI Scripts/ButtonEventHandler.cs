using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventHandler : MonoBehaviour
{
    public GameObject next_menu;
    public GameObject parent_menu;

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

    public void BeginTransition(char start_menu, char end_menu)
    {
        SlideWidgets(start_menu);
        FinishTransition(end_menu);
    }

    private IEnumerator FinishTransition(char menu)
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
        next_menu.gameObject.SetActive(true);
        SlideWidgets(menu);
    }

    private void SlideWidgets(char menu)
    {
        WidgetBehavior[] widgets = FindObjectsOfType<WidgetBehavior>();

        for (int i = 0; i < widgets.Length; i++)
        {
            if (widgets[i].menu_char == menu)
            {
                StartCoroutine(widgets[i].SlideAnimation());
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetBehavior : MonoBehaviour
{
    public float slide_duration = 1.0f;
    public float slide_distance = 100.0f;
    public char menu_char;

    private Vector3 start_pos;
    private Vector3 end_pos;

    // Start is called before the first frame update
    void Start()
    {
        start_pos = transform.position;
        end_pos = start_pos + new Vector3(slide_distance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SlideAnimation()
    {
        // move widget
        float elaspedTime = 0.0f;
        while (elaspedTime < slide_duration)
        {
            float percent = elaspedTime / slide_duration;
            transform.position = Vector3.Lerp(start_pos, end_pos, percent);
            elaspedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = end_pos;

        // switch start and end positions for next transform
        Vector3 temp = start_pos;
        start_pos = end_pos;
        end_pos = temp;
    }
}

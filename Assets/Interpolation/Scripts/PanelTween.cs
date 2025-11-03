using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelTween : MonoBehaviour
{
    public enum TweenType
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut
    }

    public TweenType tweenType = TweenType.EaseOut;
    public float tweenDuration = 1f;

    //start position.x is 540
    //end position.x is 340

    public RectTransform panel; 

    Vector3 offScreenPosition;
    Vector3 onScreenPosition;

    void Start()
    {
        // The onscreen position is set to the center of the game window.
        // Try to change that to something better, like just at the edge 
        // of the window.
        onScreenPosition = new Vector3 (340, 0, 0);
        offScreenPosition = panel.localPosition;
    }

    // Create a button, with the label Toggle Panel. You need to link the button's
    // OnClick() event to this function. 
    // Look here for how to do this: https://www.youtube.com/watch?v=Tcrg2KZck2Y
    //
    public void TogglePanel()
    {
        if (panel.localPosition == onScreenPosition)
        {
            HidePanel();
            Debug.Log("Panel hidden");
        }
        else
        {
            ShowPanel();
            Debug.Log("panel shown");
        }
    }

    public void ShowPanel()
    {
        StartCoroutine(TweenPanel(panel.localPosition, onScreenPosition, tweenDuration, tweenType));
    }

    public void HidePanel()
    {
        StartCoroutine(TweenPanel(panel.localPosition, offScreenPosition, tweenDuration, tweenType));
    }

    IEnumerator TweenPanel(Vector3 start, Vector3 end, float duration, TweenType tweenType)
    {
        float elapsedTime = 0f;

        Debug.Log("Tweening panel from " + start + " to " + end + " over " + duration + " seconds with " + tweenType + " tween type.");

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // Apply the selected tween type
            switch (tweenType)
            {
                case TweenType.Linear:
                    // No modification, t remains linear
                    break;
                case TweenType.EaseIn:
                    t = t * t; // Quadratic ease-in
                    break;
                case TweenType.EaseOut:
                    t = 1 - Mathf.Pow(1 - t, 3);
                    break;
                case TweenType.EaseInOut:
                    if (t < 0.5f)
                    {
                        // Ease-in for the first half - implement this code
                        t = 4 * t * t * t;
                    }
                    else
                    {
                        // Ease-out for the second half - implement this code
                        t = 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
                    }
                    break;
            }

            // Custom linear interpolation with modified t based on the tween type
            float x = (1 - t) * start.x + t * end.x; // Wrong value - you must add the correct equation to calculate x
            float y = 0; // Wrong value - you must add the correct equation to calculate y

            // Set the panel position
            panel.localPosition = new Vector3(x, y, start.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the panel reaches the final position
        panel.localPosition = end;
    }
}

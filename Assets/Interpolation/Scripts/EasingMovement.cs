using UnityEngine;

public class EasingMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float duration = 2.0f;

    private float elapsedTime = 0.0f;
    private Vector3 positionA;
    private Vector3 positionB;

    public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut }
    public EasingType easingType = EasingType.Linear; // Default to linear

    public float EaseInCubic(float x)
    {
        return x * x * x;
    }

    public float EaseOutCubic(float x)
    {
        return 1 - Mathf.Pow(1-x, 3);
    }

    public float EaseInOutCubic(float x)
    {
        if (x < 0.5)
        {
            return 4 * x * x * x;
        }

        else
        {
            return 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionA = pointA.position;
        positionB = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            
            t = Mathf.Clamp01(t);
            Debug.Log(t);

            // Apply an easing function to t
            // Apply the selected easing function
            switch (easingType)
            {
                case EasingType.Linear:
                    break; 
                case EasingType.EaseIn:
                    t = EaseInCubic(t);
                    break;
                case EasingType.EaseOut:
                    t = EaseOutCubic(t);
                    break;
                case EasingType.EaseInOut:
                    t = EaseInOutCubic(t);
                    break;
            } 

            // Non-linear interpolation
            Vector3 interpolatedPosition = (1 - t) * positionA + t * positionB;
            transform.position = interpolatedPosition;
        }
        else
        {
            transform.position = positionB;
        }
    }


    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            //draw point A and point B
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(pointB.position, 0.2f);

            //draw the line between points A and B
            Gizmos.color = Color.grey;
            Gizmos.DrawLine(pointA.position, pointB.position);

            //Draw Interpolation steps
            Gizmos.color = Color.green;
            int steps = 20;
            for (int i = 0; i <= steps; i++)
            {
                float t = i / (float)steps;

                // Apply the easing function in OnDrawGizmos 
                // based on the selected easingType.
                switch (easingType)
                {
                    case EasingType.Linear:
                        break;
                    case EasingType.EaseIn:
                        t = EaseInCubic(t);
                        break;
                    case EasingType.EaseOut:
                        t = EaseOutCubic(t);
                        break;
                    case EasingType.EaseInOut:
                        t = EaseInOutCubic(t);
                        break;
                }


                Vector3 interpolatedPosition = (1 - t) * positionA + t * positionB;
                Gizmos.DrawSphere(interpolatedPosition, 0.1f);
            }
        }
    }
}

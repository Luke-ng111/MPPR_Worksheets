using UnityEngine;

public class QuadraticBezier : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public GameObject p0;   // The start point GameObject
    public GameObject p1;   // The mid point GameObject
    public GameObject p2;   // The end point GameObject
    public GameObject p3;   // The mid point GameObject
    public GameObject p4;   // The end point GameObject 

    // Method to calculate a point on the quadratic Bezier curve
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1,
    Vector3 p2)
    {
        float u = 1 - t;   // u = (1 - t)
        float tt = t * t;  // t squared
        float uu = u * u;  // (1 - t) squared

        Vector3 point = uu * p0;  // (1 - t)^2 * P0
        point += 2 * u * t * p1;  // 2(1 - t)t * P1
        point += tt * p2;         // t^2 * P2

        return point;
    }

    // Function to calculate a point on a quadratic Bezier curve
    private Vector3 CalculateQuadraticBezierPoint(float t, 
    Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        return (u * u * p1) + (2 * u * t * p2) + (t * t * p3);
    }

    private void EnsureC1Continuity()
    {
        // Calculate the vector from p2 to p1
        Vector3 direction1 = p2.transform.position - p1.transform.position;

        // Set p3 to be aligned along the same direction from p2
        p3.transform.position = p2.transform.position + direction1;
    }

    private void DrawCompositeQuadraticBezierCurve()
    {
        int segmentResolution = 50;  // LineRenderer points per segment

        // Two segments, but reuse the shared point
        lineRenderer.positionCount = 2 * segmentResolution - 1;

        // Draw the first segment (p0 -> p2)
        for (int i = 0; i < segmentResolution; i++)
        {
            float t = i / (float)(segmentResolution - 1);
            Vector3 curvePoint = CalculateQuadraticBezierPoint(t, p0.transform.position, p1.transform.position, p2.transform.position);
            lineRenderer.SetPosition(i, curvePoint);
        }

        // Draw the second segment (p3, p4), reusing p2 as the start point
        for (int i = 0; i < segmentResolution; i++)
        {
            float t = i / (float)(segmentResolution - 1);
            Vector3 curvePoint = CalculateQuadraticBezierPoint(t, p2.transform.position, p3.transform.position, p4.transform.position);

            // Continue from where the first segment ended
            lineRenderer.SetPosition(i + segmentResolution - 1, curvePoint);
        }
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }

    // Method to calculate and draw the quadratic Bezier curve
    private void DrawBezierCurve()
    {
        // Number of points on the curve for smoothness
        int curveResolution = 50;  
        lineRenderer.positionCount = curveResolution;

        // Loop through each point on the curve
        for (int i = 0; i < curveResolution; i++)
        {
		    // Parameter t varies from 0 to 1
            float t = i / (float)(curveResolution - 1);  
            Vector3 curvePoint = CalculateBezierPoint(t, p0.transform.position, p1.transform.position, p2.transform.position);

            lineRenderer.SetPosition(i, curvePoint);
        }
    }

        private void Update()
        {
            // Update the curve every frame if the control points are moved
            EnsureC1Continuity();
            DrawCompositeQuadraticBezierCurve();
        }

}

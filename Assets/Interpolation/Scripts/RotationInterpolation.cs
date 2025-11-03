using UnityEngine;

public class RotationInterpolation : MonoBehaviour
{
    public Vector3 startRotationEuler = new Vector3(0, 0, 0);
    public Vector3 endRotationEuler = new Vector3(0, 180, 0);
    public float duration = 2f;

    private float elapsedTime = 0f;
    private Vector3 currentRotationEuler;

    void Start()
    {
        // Set the initial rotation
        transform.rotation = Quaternion.Euler(startRotationEuler);
        currentRotationEuler = startRotationEuler;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            t = 1 - (1 - t) * (1 - t);

            // Perform linear interpolation for each axis
            currentRotationEuler.x = (1 - t) * startRotationEuler.x + t * endRotationEuler.x;
            currentRotationEuler.y = (1 - t) * startRotationEuler.y + t * endRotationEuler.y;
            currentRotationEuler.z = (1 - t) * startRotationEuler.z + t * endRotationEuler.z;

            // Apply the interpolated rotation
            transform.rotation = Quaternion.Euler(currentRotationEuler);
        }
        else
        {
            // Ensure the exact final rotation is applied
            transform.rotation = Quaternion.Euler(endRotationEuler);
        }
    }

}

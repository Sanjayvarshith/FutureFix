using UnityEngine;

public class AmbulanceMovement : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(225.5367f, 0.08999991f, -4.782875f);
    public Vector3 endPosition = new Vector3(54.29271f, 0.08999979f, -3.960452f);
    public float speed = 10f;
    public float tireRotationSpeed = 360f; // Degrees per second

    public GameObject[] tires; // Array to hold the tire GameObjects

    private void Start()
    {
        // Set the ambulance to the starting position
        transform.position = startPosition;
    }

    private void Update()
    {
        // Move the ambulance towards the target position
        transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);

        // Rotate the tires while the ambulance is moving
        if (transform.position != endPosition)
        {
            foreach (GameObject tire in tires)
            {
                tire.transform.Rotate(Vector3.right, tireRotationSpeed * Time.deltaTime);
            }
        }

        // Optionally, stop the script if it reaches the target
        if (transform.position == endPosition)
        {
            enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;             // Speed of the platform
    public int startingPoint;       // Start index (position of the platform)
    public Transform[] points;      // Array of points the platform will move between

    private int i;      // Index for point array
    private Vector3 previousPosition; // Store the previous position of the platform

    private void Start()
    {
        transform.position = points[startingPoint].position; // Set platform position to starting point
        previousPosition = transform.position; // Initialize previous position
    }

    private void Update()
    {
        // Check distance between platform and target point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++; // Increment index
            if (i == points.Length)     // Check if platform is at the last point
            {
                i = 0;   // Reset the index
            }
        }

        // Move platform towards the current target point
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Calculate platform movement
        Vector3 deltaMovement = transform.position - previousPosition;

        // Move all objects that are currently colliding with the platform
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.transform.position += deltaMovement;
            }
        }

        // Update the previous position to the current position for next frame's calculation
        previousPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ensure player stays with the platform by updating its position each frame
            previousPosition = transform.position; // Keep track of the position when the player first touches the platform
        }
    }
}

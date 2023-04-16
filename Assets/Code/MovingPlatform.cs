using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public string moveDirection = "X"; // Public variable to define move direction ("X" or "Y")
    public float speed = 5;
    public float movingDistance = 5;
    private Vector3 originalPosition;
    private bool isMovingPositive = true;
    private Vector3 lastPlatformPosition;

    void Start()
    {
        originalPosition = transform.position;
        lastPlatformPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Move the platform back and forth based on the selected direction (X or Y)
        if (moveDirection == "X")
        {
            MoveBackAndForthOnXAxis();
        }
        else if (moveDirection == "Y")
        {
            MoveBackAndForthOnYAxis();
        }
    }

    void MoveBackAndForthOnXAxis()
    {
        // Move the platform on the X axis
        float newXPos = transform.position.x + (isMovingPositive ? speed : -speed) * Time.fixedDeltaTime;
        transform.position = new Vector2(newXPos, transform.position.y);

        // Check if the platform has moved beyond the moving distance or original position
        if (Vector3.Distance(originalPosition, transform.position) > movingDistance || transform.position.x > originalPosition.x + movingDistance / 2 || transform.position.x < originalPosition.x - movingDistance / 2)
        {
            // Reverse the direction of movement
            isMovingPositive = !isMovingPositive;
        }
    }

    void MoveBackAndForthOnYAxis()
    {
        // Move the platform on the Y axis
        float newYPos = transform.position.y + (isMovingPositive ? speed : -speed) * Time.fixedDeltaTime;
        transform.position = new Vector2(transform.position.x, newYPos);

        // Check if the platform has moved beyond the moving distance or original position
        if (Vector3.Distance(originalPosition, transform.position) > movingDistance || transform.position.y > originalPosition.y + movingDistance / 2 || transform.position.y < originalPosition.y - movingDistance / 2)
        {
            // Reverse the direction of movement
            isMovingPositive = !isMovingPositive;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Move the player along with the platform if they are standing on it
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 deltaPlatformPosition = transform.position - lastPlatformPosition;
            collision.transform.position += deltaPlatformPosition;
        }
    }

    void LateUpdate()
    {
        lastPlatformPosition = transform.position;
    }
}

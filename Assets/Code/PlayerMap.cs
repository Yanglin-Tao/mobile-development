using UnityEngine;

public class PlayerMap : MonoBehaviour
{
    public float speed = 10.0f; // the speed of the cursor movement
    public float delay = 0.1f; // the delay before the cursor starts moving
    public float offset = 0.5f; // the offset from the touch position

    private Vector3 targetPosition;
    private bool isFacingRight = true;
    private bool followCursor = true;

    private void Start()
    {
        targetPosition = transform.position; // initialize the target position to the cursor's starting position
    }

    public void StopFollowingCursor() {
        followCursor = false;
    }

    private void Update()
    {
        // check if there are any touches on the screen
        if (Input.touchCount > 0 && followCursor)
        // if (Input.touchCount > 0)
        {
            // get the position of the first touch
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            // add the offset to the touch position and add the delay to the cursor movement
            targetPosition = new Vector3(touchPosition.x + offset, touchPosition.y + offset, transform.position.z);
            Invoke("MoveCursor", delay);

            // flip the cursor if the touch position is to the left or right of the cursor
            if (touchPosition.x < transform.position.x && isFacingRight)
            {
                FlipCursor();
            }
            else if (touchPosition.x > transform.position.x && !isFacingRight)
            {
                FlipCursor();
            }
        }

        // move the cursor towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    
    private void MoveCursor()
    {
        // check if there are any touches on the screen
        if (Input.touchCount > 0)
        {
            // set the target position to the touch position with the offset
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            targetPosition = new Vector3(touchPosition.x + offset, touchPosition.y + offset, transform.position.z);

            // flip the cursor if the touch position is to the left or right of the cursor
            if (touchPosition.x < transform.position.x && isFacingRight)
            {
                FlipCursor();
            }
            else if (touchPosition.x > transform.position.x && !isFacingRight)
            {
                FlipCursor();
            }
        }
    }

    private void FlipCursor()
    {
        // flip the cursor's sprite
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }
}

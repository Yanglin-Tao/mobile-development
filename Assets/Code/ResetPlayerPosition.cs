using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    // position to trigger reset
    public float triggerYPosition = 0;
    // position after resetting
    public float resetYPosition = 0;
    public float resetXPosition = 0;

    // Update is called once per frame
    void Update()
    {
        // if player falls below a certain point,
        // reset to resetXPosition and resetYPosition
        if (transform.position.y < triggerYPosition)
        {
            transform.position = new Vector3(resetXPosition, resetYPosition, transform.position.z);
        }
    }
}

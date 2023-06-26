using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public float resetPosition = 10.0f;

    private Vector3 startPosition;

    private bool active;

    private void Start()
    {
        startPosition = transform.position;
    }

    public void StopParallax()
    {
        active = false;
    }
    public void StartParallax()
    {
        active = true;
    }

    private void Update()
    {
        if (active)
        {
            // transform.position = new Vector3(100, 0, )
            // Move the GameObject along the X-axis based on the scroll speed
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            // If the GameObject has moved past the reset position, reset its position to the initial position
            if (transform.localPosition.x <= resetPosition)
            {
                Debug.Log(transform.position);
                transform.position = startPosition;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLook : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float my_gravity;

    [SerializeField]
    Rigidbody player;
    public Vector3 velocity;
    float _angle;

    public float minAngle = -45f;
    public float maxAngle = 0f;


    private float lastY, currentY;

    [SerializeField]
    private float rotationSpeed = 2;
    private Vector3 currentEulerAngles;
    private void Start()
    {
        currentEulerAngles = transform.rotation.eulerAngles;
    }

    private void FallSpeed()
    {
        velocity.y += my_gravity * Time.deltaTime;
        // I assume this is intended to apply both y AND x movement?
        player.transform.position += velocity * Time.deltaTime;

        // Convert velocity to an angle. (If velocity.x is 0, use 1 instead)
        float newAngle = Mathf.Atan2(velocity.y, velocity.x);

        // Convert to degrees, and clamp between your desired range.
        newAngle = Mathf.Clamp(newAngle * Mathf.Rad2Deg, minAngle, maxAngle);

        // Blend from your old angle toward the new angle, smoothly.
        _angle = Mathf.Lerp(_angle, newAngle, Time.deltaTime);

        // Set your rotation to this angle.
        player.transform.localEulerAngles = new Vector3(0, 0, _angle);
    }

    // private void FixedUpdate()
    // {  
    //     currentY = player.transform.position.y;

    //     float playerVelocity = currentY - lastY;

    //     lastY = currentY;

    //     print(playerVelocity);
        
    //     // currentEulerAngles += new Vector3(0, 0, Mathf.Sign(playerVelocity)) * Time.deltaTime * rotationSpeed;
    //     Vector3 lerpedValue;
    //     // if (playerVelocity > 0)
    //     // {
    //     //     lerpedValue = Vector3.Lerp(new Vector3(0, 0, Mathf.Sign(playerVelocity)), transform.rotation.eulerAngles, 0.1f);
    //     // }
    //     // else
    //     // {
    //     //     lerpedValue = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, Mathf.Sign(playerVelocity)), 0.1f);
    //     // }

    //     // lerpedValue = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(0, 0, Mathf.Sign(playerVelocity) * 45), 0.1f);
    //     // transform.rotation = Quaternion.Euler(lerpedValue);
       
    //     transform.Rotate(new Vector3(0, 0, playerVelocity));
    //     Debug.Log(transform.rotation.z);
    // }

    float r = 0f;
    void Update()
    {
        currentY = player.transform.position.y;

        float playerVelocity = currentY - lastY;

        lastY = currentY;
        //Basically lerp
        float s = playerVelocity;
        print("Velocity = " + s);
        r = r - (r - s * 45) * 0.05f;
        transform.rotation = Quaternion.Euler(0, 0, r);
        // print(Mathf.Sign(s));
    }

    // Update is called once per frame
}

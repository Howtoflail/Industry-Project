using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float obstacleSpeed;

    [SerializeField]
    private float destroyLocationX;

    public bool frozen = false;

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            transform.position += ((Vector3.left * obstacleSpeed) * Time.deltaTime);
            // Debug.Log(transform.position.x);
        }
    }

    public void Freeze()
    {
        frozen = true;
    }

    public float GetX()
    {
        return transform.position.x;
    }

    public void SetSpeed(float newSpeed)
    {       
        obstacleSpeed = newSpeed;
    }
}

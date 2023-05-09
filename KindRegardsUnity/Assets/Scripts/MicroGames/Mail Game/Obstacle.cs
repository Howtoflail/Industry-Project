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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += ((Vector3.left * obstacleSpeed) * Time.deltaTime);
        Debug.Log(transform.position.x);
        if (transform.position.x < destroyLocationX)
        {
            Destroy(gameObject);
        }
    }
}

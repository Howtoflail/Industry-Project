using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailGameObstacles : MonoBehaviour
{
    public GameObject topObstacle;
    public GameObject bottomObstacle;
    public float obstacleSpawnPositionX;

    [SerializeField]
    private float gap;
    [SerializeField]
    private float timeBetweenObstacles;
    private float nextSpawnTime;
    private float height;

    private bool inGame;

    void Start()
    {
        inGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            if (Time.time > nextSpawnTime)
            {
                nextSpawnTime += timeBetweenObstacles;

                GameObject top = Instantiate(topObstacle, GameObject.FindGameObjectWithTag("Canvas").transform);
                GameObject bottom = Instantiate(bottomObstacle, GameObject.FindGameObjectWithTag("Canvas").transform);

                height = GenerateRandomHeight();
                top.transform.position = transform.position + new Vector3(obstacleSpawnPositionX, (height+gap), 0);
                bottom.transform.position = transform.position + new Vector3(obstacleSpawnPositionX, (height-gap), 0);

                // top.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                // bottom.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);


            }
        }
        else { }
    }

    private float GenerateRandomHeight()
    {
        return Random.Range(-400.0f, 400.0f);
    }

    public void StartSpawning()
    {
        inGame = true;
    }


    public void StopSpawning() { }
}

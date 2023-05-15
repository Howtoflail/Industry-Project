using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailGameObstacles : MonoBehaviour
{
    public GameObject topObstacle;
    public GameObject bottomObstacle;
    public GameObject playerDetector;
    public float obstacleSpawnPositionX;

    [SerializeField]
    private float gap;
    [SerializeField]
    private float maxGapHeight;
    [SerializeField]
    private float minGapHeight;

    [SerializeField]
    private float timeBetweenObstacles;
    private float nextSpawnTime;
    private float height;
    private bool inGame;

    List<GameObject> obstacles;
    List<GameObject> playerDetectors;


    void Start()
    {
        inGame = false;
        obstacles = new List<GameObject>();
        playerDetectors = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            if (Time.time > nextSpawnTime)
            {
                nextSpawnTime += timeBetweenObstacles;

                GameObject top = Instantiate(
                    topObstacle,
                    GameObject.FindGameObjectWithTag("Canvas").transform
                );
                GameObject bottom = Instantiate(
                    bottomObstacle,
                    GameObject.FindGameObjectWithTag("Canvas").transform
                );
                GameObject detector= Instantiate(
                    playerDetector,
                    GameObject.FindGameObjectWithTag("Canvas").transform
                );

                obstacles.Add(top);
                obstacles.Add(bottom);
                playerDetectors.Add(detector);
                height = GenerateRandomHeight();
                top.transform.position =
                    transform.position + new Vector3(obstacleSpawnPositionX, (height + gap), 0);
                bottom.transform.position =
                    transform.position + new Vector3(obstacleSpawnPositionX, (height - gap), 0);
                detector.transform.position =
                    transform.position + new Vector3(obstacleSpawnPositionX, (height), 0); 

                // top.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                // bottom.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            }
        }
        else { }
    }

    private float GenerateRandomHeight()
    {
        return Random.Range(minGapHeight, maxGapHeight);
    }

    public void StartSpawning()
    {
        inGame = true;
    }

    public void FreezeAll()
    {
        inGame = false;
        foreach (GameObject obstacleObject in obstacles)
        {
            obstacleObject.GetComponent<Obstacle>().Freeze();
        }
    }
}

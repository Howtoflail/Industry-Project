using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MailGameObstacles : MonoBehaviour
{
    [SerializeField]
    private GameObject topObstacle;

    [SerializeField]
    private GameObject bottomObstacle;

    [SerializeField]
    private GameObject playerDetector;

    [SerializeField]
    private GameObject endPrefab;

    [SerializeField]
    private float obstacleSpawnPositionX;

    [SerializeField]
    private float gap;

    [SerializeField]
    private float maxGapHeight;

    [SerializeField]
    private float minGapHeight;

    [SerializeField]
    private float timeBetweenObstacles;

    [SerializeField]
    private float destroyLocationX;

    [SerializeField]
    private int obstaclesBeforeEnd;
    private float nextSpawnTime;
    private float height;
    private bool inGame;
    private bool endSpawned;
    private int obstaclesSpawned = 0;

    List<GameObject> obstacles;
    List<GameObject> playerDetectors;

    private bool spawnEnding;

    void Start()
    {
        inGame = false;
        obstacles = new List<GameObject>();
        playerDetectors = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obstaclesSpawned > obstaclesBeforeEnd)
        {
            spawnEnding = true;
        }
        if (inGame)
        {
            if (Time.time > nextSpawnTime)
            {
                if (spawnEnding)
                {
                    SpawnEnding();
                }
                else
                {
                    SpawnObstacles();
                    obstaclesSpawned++;
                }
            }
            foreach (GameObject item in obstacles.ToList())
            {
                if (item.transform.position.x <= destroyLocationX)
                {
                    obstacles.Remove(item);
                    Destroy(item);
                }
            }
            foreach (GameObject item in playerDetectors.ToList())
            {
                if (item.transform.position.x <= destroyLocationX)
                {
                    playerDetectors.Remove(item);
                    Destroy(item);
                }
            }
        }
    }

    private void SpawnEnding()
    {
        if (!endSpawned)
        {
            GameObject endObject = Instantiate(
                endPrefab,
                GameObject.FindGameObjectWithTag("Canvas").transform.Find("MailGame/Win")
            );
            endObject.transform.position =
                transform.position + new Vector3(obstacleSpawnPositionX, 0, 0);
            endSpawned = true;
        }
    }

    private void SpawnObstacles()
    {
        nextSpawnTime += timeBetweenObstacles;

        GameObject detector = Instantiate(
            playerDetector,
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("MailGame/Detectors")
        );

        playerDetectors.Add(detector);

        SpawnObstaclesBottom();
        SpawnObstaclesTop();

        detector.transform.position =
            transform.position + new Vector3(obstacleSpawnPositionX, 0, 0);

        // top.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        // bottom.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        return;
    }

    private float GenerateRandomHeight()
    {
        return Random.Range(minGapHeight, maxGapHeight);
    }

    private void SpawnObstaclesBottom()
    {
        GameObject bottom = Instantiate(
            bottomObstacle,
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("MailGame/Obstacles")
        );
        obstacles.Add(bottom);
        bottom.transform.position =
            transform.position + new Vector3(obstacleSpawnPositionX, -20, 0);
    }

    private void SpawnObstaclesTop()
    {
        height = GenerateRandomHeight();
        GameObject top = Instantiate(
            topObstacle,
            GameObject.FindGameObjectWithTag("Canvas").transform.Find("MailGame/Obstacles")
        );
        obstacles.Add(top);
        top.transform.position =
            transform.position + new Vector3(obstacleSpawnPositionX + Random.Range(-10f, 10f), (height + gap), 0);
    }

    public void StartSpawning()
    {
        inGame = true;
        nextSpawnTime = Time.time + timeBetweenObstacles;
    }

    public void FreezeAll()
    {
        inGame = false;
        foreach (GameObject obstacleObject in obstacles)
        {
            obstacleObject.GetComponent<Obstacle>().Freeze();
        }
        foreach (GameObject detectorObject in playerDetectors)
        {
            detectorObject.GetComponent<Obstacle>().Freeze();
        }
    }

    public void DestroyAllObstacles()
    {
        foreach (GameObject obstacleObject in obstacles)
        {
            Destroy(obstacleObject);
        }
        foreach (GameObject detectorObject in playerDetectors)
        {
            Destroy(detectorObject);
        }
        obstacles.Clear();
        playerDetectors.Clear();
        obstaclesSpawned = 0;
        spawnEnding = false;
        endSpawned = false;
    }

    public void DestoryObstacle(GameObject obstacle)
    {
        obstacles.Remove(obstacle);
    }
}

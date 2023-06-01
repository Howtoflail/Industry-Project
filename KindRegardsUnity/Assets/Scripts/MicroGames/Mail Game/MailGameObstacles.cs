using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [SerializeField]
    private float destroyLocationX;
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
                    GameObject.FindGameObjectWithTag("Canvas").transform.Find("MailGame/Obstacles")
                );
                GameObject bottom = Instantiate(
                    bottomObstacle,
                    GameObject.FindGameObjectWithTag("Canvas").transform.Find("MailGame/Obstacles")
                );
                GameObject detector = Instantiate(
                    playerDetector,
                    GameObject.FindGameObjectWithTag("Canvas").transform.Find("MailGame/Detectors")
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

    private float GenerateRandomHeight()
    {
        return Random.Range(minGapHeight, maxGapHeight);
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
    }

    public void DestoryObstacle(GameObject obstacle)
    {
        obstacles.Remove(obstacle);
    }
}

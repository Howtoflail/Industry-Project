using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailGameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inGame;
    private int score = 0;

    MailGamePlayer player;
    MailGameObstacles obstacles;
    public GameObject startButton;

    void Start()
    {
        obstacles = GameObject.Find("MailGame").GetComponent<MailGameObstacles>();
        player = GameObject.Find("MailGamePlayer").GetComponent<MailGamePlayer>();
        inGame = false;
        player.gameObject.SetActive(false);
        startButton.SetActive(true);
        return;
    }

    // Update is called once per frame
    void Update() { }

    public void StartMailGame()
    {
        startButton.SetActive(false);
        player.gameObject.SetActive(true);
        obstacles.DestroyAllObstacles();
        player.ResetPosition();

        inGame = true;
        return;
    }

    public void AddScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        inGame = false;
        startButton.SetActive(true);
    }
}

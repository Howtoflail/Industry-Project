using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MailGameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inGame;
    private int score = 0;

    MailGamePlayer player;
    MailGameObstacles obstacles;
    Background background;
    public GameObject startButton;

    void Start()
    {
        obstacles = GameObject.Find("MailGame").GetComponent<MailGameObstacles>();
        player = GameObject.Find("MailGamePlayer").GetComponent<MailGamePlayer>();
        background = GameObject.Find("MailGameBackground").GetComponent<Background>();
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
        obstacles.StartSpawning();
        background.StartParallax();
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
        obstacles.FreezeAll();
        background.StopParallax();
    }

    public void Win()
    {
        SceneManager.LoadScene(2);
    }
}

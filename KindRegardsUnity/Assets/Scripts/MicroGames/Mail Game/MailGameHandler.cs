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

    void Start()
    {
        obstacles = GameObject.Find("MailGame").GetComponent<MailGameObstacles>();
        player = GameObject.Find("MailGame").GetComponent<MailGamePlayer>();
        inGame = false;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            
        }
    }

    public void StartMailGame()
    {
        inGame = true;
        return;
    }
    public void AddScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }
}

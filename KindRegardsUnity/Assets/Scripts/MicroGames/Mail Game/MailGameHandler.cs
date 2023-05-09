using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailGameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inGame;
    void Start()
    {
        inGame = false;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            return;
        }
    
    }

    public void StartMailGame()
    {
        inGame = true;
        return;
    }
}

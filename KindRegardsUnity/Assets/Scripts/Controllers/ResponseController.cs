using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseController : MonoBehaviour
{
    public TextMeshProUGUI output;
    [SerializeField] public GameObject panelResponse;
    [SerializeField] public GameObject mail;
    [SerializeField] private bool messageOpen;
    [SerializeField] private GameObject panelReply;
    private bool replyOpen;

    void Start()
    {
        messageOpen = false;
        replyOpen = false;
    }
    
    public void HandleInputData(int response)
    {
        switch(response)
        {
            case 0:
            output.text = "Je kunt het!";
            break;

            case 1:
            output.text = "Alles komt goed!";
            break;

            case 2:
            output.text = "Je bent niet alleen!";
            break;
        }
    }

    public void OpenMessage()
    {
        if (messageOpen == false)
        {
            panelResponse.SetActive(true);
            messageOpen = true;
        }
    }

    public void OpenReply()
    {
        if (replyOpen == false)
        {
            panelReply.SetActive(true);
            replyOpen = true;
        }
    }

    private IEnumerator CloseMessageScreen()
    {
        panelResponse.SetActive(false);
        float waitTime = 4;
        yield return new WaitForSeconds(waitTime);
        messageOpen = false;
    }

    private IEnumerator CloseReplyScreen()
    {
        panelReply.SetActive(false);
        float waitTime = 4;
        yield return new WaitForSeconds(waitTime);
        replyOpen = false;
    }

    public void CloseMessage()
    {
        StartCoroutine(CloseMessageScreen());
    }

    public void CloseReply()
    {
        StartCoroutine(CloseReplyScreen());
    }
}
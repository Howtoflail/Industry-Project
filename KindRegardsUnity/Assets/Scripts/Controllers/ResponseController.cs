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
    [SerializeField] private GameObject panelSendMessage;
    [SerializeField] private TextMeshProUGUI messageOutput;
    private bool replyOpen;
    private bool sendMessage;

    void Start()
    {
        messageOpen = false;
        replyOpen = false;
        sendMessage = false;
    }
    
    public void HandleInputDataReply(int response)
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

    public void HandleInputDataMessage(int response)
    {
        switch(response) 
        {
            case 0:
                messageOutput.text = "Mijn dag is behoorlijk slecht geweest.";
                break;
            case 1:
                messageOutput.text = "Vandaag was een vreemde dag.";
                break;
            case 2:
                messageOutput.text = "Ik had een goede dag vandaag.";
                break;
        }
    }

    public void OpenSendMessage()
    {
        if (sendMessage == false)
        {
            panelSendMessage.SetActive(true);
            sendMessage = true;
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

    private IEnumerator CloseSendMessageScreen()
    {
        panelSendMessage.SetActive(false);
        float waitTime = 4;
        yield return new WaitForSeconds(waitTime);
        sendMessage = false;
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

    public void CloseSendMessage()
    {
        StartCoroutine(CloseSendMessageScreen());
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseController : MonoBehaviour
{
    public TextMeshProUGUI output;
    [SerializeField] public GameObject panelResponse;
    [SerializeField] public GameObject mail;
    [SerializeField] private bool open;

    void Start()
    {
        open = false;
    }

    void Update()
    {
        if(mail.active == true)
        {
            if(Input.GetKey("mouse 0"))
            {
                OpenMessage();
            }
        }
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
        if(open == false)
            {
                panelResponse.active = true; 
                open = true;
            }
    }

    public IEnumerator CloseScreen()
    {
        panelResponse.active = false;
        float waitTime = 4;
        yield return new WaitForSeconds(waitTime);
        open = false;
    }

    public void CloseMessage()
    {
        StartCoroutine(CloseScreen());
    }
}
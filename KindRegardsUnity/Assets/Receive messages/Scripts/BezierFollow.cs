using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    [SerializeField]
    private GameObject messageHandlingObject;

    [SerializeField]
    private GameObject[] uiElements;

    private MessageHandling messageHandling;

    private int routeToGo;

    private float tParam;

    private Vector3 objectPosition;

    private float speedModifier;

    private bool coroutineAllowed;

    private float timeWhenMessageSent = 0f;

    [SerializeField]
    private float timeToWaitForSendingMessageAnimation = 6f;

    private float timeWhenSentAnimationFinished = 0f;

    private float timeWhenArriveAnimationFinished = 0f;

    void Start()
    {
        messageHandling = messageHandlingObject.GetComponent<MessageHandling>();
        tParam = 0f;
        speedModifier = 0.25f;
        coroutineAllowed = true;

        foreach(GameObject uiElement in uiElements) 
        {
            uiElement.SetActive(false);
        }
    }

    void Update()
    {
        if(timeWhenSentAnimationFinished != 0f) 
        {
            timeWhenSentAnimationFinished = 0f;
            //load the mail game
            SceneManager.LoadScene(1);
        }

        if(timeWhenArriveAnimationFinished != 0f)
        {
            timeWhenArriveAnimationFinished = 0f;
            //Enable the buttons
            foreach (GameObject uiElement in uiElements)
            {
                uiElement.SetActive(true);
            }
        }
    }

    public void OnClickMove()
    {
        if (coroutineAllowed)
        {
            routeToGo = Random.Range(0, 3); //This can return 0, 1 or 2
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    public void SendMessageAfterWriting()
    {
        messageHandling.SendMessage().ContinueWithOnMainThread((task) => 
        {
            Debug.Log($"Sending a message returns: {task.Result}");

            if (coroutineAllowed && task.Result)
            {
                Debug.Log($"If check works");
                timeWhenMessageSent = Time.time;
                routeToGo = 3;
                StartCoroutine(GoByTheRoute(routeToGo));
            }
        });
    }


    private IEnumerator GoByTheRoute(int routeNumber)
    {
        if(timeWhenMessageSent != 0f)
        {
            yield return new WaitForSeconds(timeToWaitForSendingMessageAnimation);
        }

        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 
            3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 
            3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + 
            Mathf.Pow(tParam, 3) * p3;

            //makes the object look at where it's going
            Vector3 dir = objectPosition - transform.position;
            dir.y = 0;                                          // if you want in sepecific directions only or its optional 
            transform.rotation = Quaternion.LookRotation(dir);
            transform.position = objectPosition; 

            transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0;
        speedModifier = speedModifier * 0.90f;
        // routeToGo += 1;

        // if (routeToGo > routes.Length - 1)
        // {
        //     routeToGo = 0;
        // }

        coroutineAllowed = true;
        if(routeNumber == 3) 
        {
            timeWhenSentAnimationFinished = Time.time;
            Debug.Log($"Time when animation finished: {timeWhenSentAnimationFinished}");
        }
        else
        {
            timeWhenArriveAnimationFinished = Time.time;
            Debug.Log($"Time when animation finished: {timeWhenArriveAnimationFinished}");
        }
    }
}

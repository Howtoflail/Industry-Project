using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpeed : MonoBehaviour
{
    Animator animatorPaperPlane;

    // Start is called before the first frame update
    void Start()
    {
        animatorPaperPlane = gameObject.GetComponent<Animator>();
        animatorPaperPlane.SetFloat("Speed", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //if a player puts in a certain type of emotion then the speed of the throw is adjusted

        //it is now made with keydowns, but it could be linked to the emotion wheel later
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("emotiongroup1");
            //Emotions like angry and annoyed
            animatorPaperPlane.SetFloat("Speed", 2f);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("emotiongroup2");
            //Emotions like happy and proud
            animatorPaperPlane.SetFloat("Speed", 1.5f);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("emotiongroup3");
            //Emotions like tired and sad
            animatorPaperPlane.SetFloat("Speed", 1f);
        }
    }
}

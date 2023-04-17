using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class AnimationController : MonoBehaviour
{
    public GameObject paperPlaneObj;

    public void PlayFlyingAnimation()
    {
        //random generation of numbers so the animations will be different
        Random randomNumber = new Random();
        int randomStateAnimation = randomNumber.Next(0, 4);

        //switch case to select one of the animations
        switch (randomStateAnimation)
        {
            case 0:
                paperPlaneObj.GetComponent<Animator>().SetTrigger("Sent");
                break;
            case 1:
                paperPlaneObj.GetComponent<Animator>().SetTrigger("Sent2");
                break;
            case 2:
                paperPlaneObj.GetComponent<Animator>().SetTrigger("Sent3");
                break;
            case 3:
                paperPlaneObj.GetComponent<Animator>().SetTrigger("Sent4");
                break;
            case 4:
                paperPlaneObj.GetComponent<Animator>().SetTrigger("Sent5");
                break;
            default:
                break;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private CameraAnimationController cameraAnimation;
    void Start()
    {
        cameraAnimation = GameObject.Find("CM vcam").GetComponent<CameraAnimationController>();
    }

    public void NewPosition(int newState)
    {
        StartCoroutine(cameraAnimation.ChangePosition(newState));
    }

    public void Return()
    {
        int uiState = (int)GameObject.Find("UI Canvas").GetComponent<UIController>().GetCurrentUIState();
        if (cameraAnimation.GetState().state == uiState)
        {
            StartCoroutine(cameraAnimation.ChangePosition(0));
        }
    }
}

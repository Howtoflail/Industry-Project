using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIState : MonoBehaviour
{
    [SerializeField] List<UIStateEnum> uiStates;
    
    public void DetectActive(UIStateEnum currentState)
    {
        if (uiStates.Contains(currentState))
        {
            gameObject.SetActive(true);
            Debug.Log($"Active state now is: {currentState}");
        }
        else
        {
            gameObject.SetActive(false);
        }
            
    }
}

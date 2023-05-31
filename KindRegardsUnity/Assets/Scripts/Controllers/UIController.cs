using System;
using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class UIController : MonoBehaviour
{
    private List<UIStateEnum> uiStates;
    private UnityEngine.Object[] states;
    [SerializeField] private GameObject menuOptions;
    private UIAnimationController animationController;
    private bool collapsed;

    private void Start()
    {
        animationController = GetComponent<UIAnimationController>();

        uiStates = new List<UIStateEnum>();
        states = Resources.FindObjectsOfTypeAll(typeof(UIState));
        //menuOptions = GameObject.Find("Optionsbg");
        Forward(0);
        Forward(13);
    }
    public void NewUser()
    {
        collapsed = true;
        //menuOptions = GameObject.Find("Optionsbg");     
        menuOptions.SetActive(false);
    }

    private void Navigate(UIStateEnum uiState)
    {
        foreach (UIState state in states) state.DetectActive(uiState);
    }

    private void AddUIState(UIStateEnum newUIState)
    {
        uiStates.Add(newUIState);
    }

    private void RemoveUIState()
    {
        uiStates.Remove(GetCurrentUIState());
    }

    public UIStateEnum GetCurrentUIState()
    {
        Debug.Log($"GetCurrentUIState function returns: {uiStates[uiStates.Count - 1]}");
        return uiStates[uiStates.Count - 1];
    }

    public void Forward(int uiState)
    {
        if (!collapsed) MenuClick();
        AddUIState((UIStateEnum)uiState);
        Navigate(GetCurrentUIState());
    }

    public void Back()
    {
        Debug.Log($"Back function removes: {GetCurrentUIState()}");
        RemoveUIState();
        Debug.Log($"And then navigates to: {GetCurrentUIState()}");
        Navigate(GetCurrentUIState());

    }

    public void MenuClick()
    {
        collapsed = !collapsed;
        if (!collapsed) menuOptions.SetActive(true);
        
        StartCoroutine(animationController.MenuAnimation(menuOptions, collapsed));
        if (collapsed) menuOptions.SetActive(false);
    }
    public void HideMenu()
    {
        collapsed = true;
        menuOptions.SetActive(false);
    }
    public void ShowMenu()
    {
        collapsed = false;
        menuOptions.SetActive(true);
    }
}

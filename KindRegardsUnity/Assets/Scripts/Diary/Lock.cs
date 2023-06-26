using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    private bool isCodeSet;
    private string code;

    public Lock()
    {
        isCodeSet = false;
        return;
    }

    public bool GetIfCodeSet()
    {
        return isCodeSet;
    }

    public void SetIfCodeSet(bool value)
    {
        isCodeSet = value;
    }

    public string GetCode()
    {
        return code;
    }

    public bool CodeCorrect(string enteredCode)
    {
        if (code == enteredCode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetCode(string value)
    {
        code = value;
    }
}

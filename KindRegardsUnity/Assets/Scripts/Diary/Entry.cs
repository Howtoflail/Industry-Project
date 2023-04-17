using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Entry
{
    private string date;
    private string content;
    private string emotion;

    public void SetContent(string content)
    {
        this.content = content;
    }

    public string GetEmotion()
    {
        return emotion;
    }

    public void SetEmotion(string emotion)
    {
        this.emotion = emotion;
    }

    public string GetContent()
    {
        return content;
    }

    public void SetDate()
    {
        DateTime dateTime = DateTime.Now;
        String[] splitVar = dateTime.ToString().Split(' ');

        this.date = splitVar[0];
    }

    public string GetDate()
    {
        return date;
    }
}

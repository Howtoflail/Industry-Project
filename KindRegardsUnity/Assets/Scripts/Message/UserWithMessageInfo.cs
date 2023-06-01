using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserWithMessageInfo
{
    private string name;
    private bool isActive;
    private DateTime lastTimeLoggedIn;
    private DateTime lastTimeMessageReceived;
    private object lastTimeMessageReceivedTimeStamp; //only to be used when updating a user in database
    private int messagesReceivedPerDay;

    public string Name { get { return name; } set { name = value; } }
    public bool IsActive { get { return isActive; } set { isActive = value; } }
    public DateTime LastTimeLoggedIn { get { return lastTimeLoggedIn; } set { lastTimeLoggedIn = value; } }
    public DateTime LastTimeMessageReceived { get { return lastTimeMessageReceived; } set { lastTimeMessageReceived = value;} }
    public object LastTimeMessageReceivedTimeStamp { get { return lastTimeMessageReceivedTimeStamp; } set { lastTimeMessageReceivedTimeStamp = value;} }
    public int MessagesReceivedPerDay { get { return messagesReceivedPerDay; } set { messagesReceivedPerDay = value; } }

    public UserWithMessageInfo(string name, bool isActive, DateTime lastTimeLoggedIn, DateTime lastTimeMessageReceived, int messagesReceivedPerDay)
    {
        this.name = name;
        this.isActive = isActive;
        this.lastTimeLoggedIn = lastTimeLoggedIn;
        this.lastTimeMessageReceived = lastTimeMessageReceived;
        this.messagesReceivedPerDay = messagesReceivedPerDay;
    }
}

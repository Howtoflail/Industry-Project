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
    private DateTime lastTimeMessageSent;
    private object lastTimeLoggedInTimeStamp; //only to be used when updating a user in database
    private object lastTimeMessageReceivedTimeStamp; //only to be used when updating a user in database
    private object lastTimeMessageSentTimeStamp; //only to be used when updating a user in database
    private int messagesReceivedPerDay;

    public string Name { get { return name; } set { name = value; } }
    public bool IsActive { get { return isActive; } set { isActive = value; } }
    public DateTime LastTimeLoggedIn { get { return lastTimeLoggedIn; } set { lastTimeLoggedIn = value; } }
    public DateTime LastTimeMessageReceived { get { return lastTimeMessageReceived; } set { lastTimeMessageReceived = value;} }
    public DateTime LastTimeMessageSent { get { return lastTimeMessageSent; } set { lastTimeMessageSent = value; } }
    public object LastTimeLoggedInTimeStamp { get { return lastTimeLoggedInTimeStamp; } set { lastTimeLoggedInTimeStamp = value; } }
    public object LastTimeMessageReceivedTimeStamp { get { return lastTimeMessageReceivedTimeStamp; } set { lastTimeMessageReceivedTimeStamp = value;} }
    public object LastTimeMessageSentTimeStamp { get { return lastTimeMessageSentTimeStamp; } set { lastTimeMessageSentTimeStamp = value; } }
    public int MessagesReceivedPerDay { get { return messagesReceivedPerDay; } set { messagesReceivedPerDay = value; } }

    public UserWithMessageInfo(string name, bool isActive, DateTime lastTimeLoggedIn, DateTime lastTimeMessageReceived, DateTime lastTimeMessageSent, int messagesReceivedPerDay)
    {
        this.name = name;
        this.isActive = isActive;
        this.lastTimeLoggedIn = lastTimeLoggedIn;
        this.lastTimeMessageReceived = lastTimeMessageReceived;
        this.lastTimeMessageSent = lastTimeMessageSent;
        this.messagesReceivedPerDay = messagesReceivedPerDay;
    }
}

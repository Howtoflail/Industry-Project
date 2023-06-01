using System;
using System.Collections;
using System.Collections.Generic;

//Class for displaying messages in the UI
public class Message
{
    private string name;
    private string textMessage;
    private DateTime timestamp;

    public string Name { get { return name; } set { name = value; } }
    public string TextMessage { get { return textMessage; } set { textMessage = value; } }
    public DateTime Timestamp { get { return timestamp; } set { timestamp = value; } }

    public Message(string name, string message, DateTime timestamp)
    {
        this.name = name;
        textMessage = message;
        this.timestamp = timestamp;
    }
}

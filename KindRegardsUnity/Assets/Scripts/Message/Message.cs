using System;
using System.Collections;
using System.Collections.Generic;

//Class for displaying messages in the UI
public class Message
{
    private string id;
    private string from;
    private List<string> to;
    private string name;
    private string textMessage;
    private DateTime timestamp;

    public string Id { get { return id; }  set { id = value; } }
    public string From { get { return from; } set { from = value; } }
    public List<string> To { get { return to; } set { to = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string TextMessage { get { return textMessage; } set { textMessage = value; } }
    public DateTime Timestamp { get { return timestamp; } set { timestamp = value; } }

    public Message(string id, string from, List<string> to, string name, string message, DateTime timestamp)
    {
        this.id = id;
        this.from = from;
        this.to = to;
        this.name = name;
        textMessage = message;
        this.timestamp = timestamp;
    }
}

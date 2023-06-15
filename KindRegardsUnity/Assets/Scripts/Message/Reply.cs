using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reply 
{
    private string id;
    private string name;
    private string from;
    private string to;
    private string messageId;
    private string textMessage;
    private DateTime timestamp;
    private bool read;

    public string Id { get { return id; } set { id = value; } }
    public string Name { get { return name; } set { name = value; } }
    public string From { get { return from; } set { from = value; } }
    public string To { get { return to; } set { to = value; } }
    public string MessageId { get { return messageId; } set { messageId = value; } }
    public string TextMessage { get { return textMessage; } set { textMessage = value;} }
    public DateTime Timestamp { get { return timestamp; } set { timestamp = value; } }
    public bool Read { get { return read; } set { read = value; } }

    public Reply(string id, string name, string from, string to, string messageId, string textMessage, DateTime timestamp, bool read)
    {
        this.id = id;
        this.name = name;
        this.from = from;
        this.to = to;
        this.messageId = messageId;
        this.textMessage = textMessage;
        this.timestamp = timestamp;
        this.read = read;
    }
}

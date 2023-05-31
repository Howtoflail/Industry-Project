using System.Collections;
using System.Collections.Generic;

//Class for displaying messages in the UI
public class Message
{
    private string name;
    private string textMessage;
    private string timestamp;

    public string Name { get { return name; } set { name = value; } }
    public string TextMessage { get { return textMessage; } set { textMessage = value; } }
    public string Timestamp { get { return timestamp; } set { timestamp = value; } }

    public Message(string name, string message, string timestamp)
    {
        this.name = name;
        this.textMessage = message;
        this.timestamp = timestamp;
    }
}

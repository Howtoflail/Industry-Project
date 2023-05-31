using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MessageHandling : MonoBehaviour
{
    private string filePath = @"User.bin";
    private List<string> messageIds = new List<string>();
    private List<Message> messages = new List<Message>();
    private List<(GameObject, GameObject)> setsOfMessages = new List<(GameObject, GameObject)>();
    private string userId;
    private string userName;
    private bool ok = false;

    [SerializeField]
    private Text textMessagesCounter;
    [SerializeField]
    private GameObject messageBox;
    [SerializeField]
    private GameObject textMessagePrefab;
    [SerializeField]
    private GameObject textMessageNamePrefab;
    [SerializeField] 
    private GameObject panelResponse;

    private UserHandling userHandling;
    private FirebaseFirestore firestore;

    //Messages in the db should have:
    //name
    //text
    //timestamp
    //from - id of the user that sent the message
    //to - a list of id's of all the players
    //repliedBy - a list of id's of all the players that replied to the message
    //sticker - to be done after stickers are implemented
    //
    //New messages are messages that haven't been replied to by a player yet
    //and they should be able to be seen everytime a player goes to mailbox
    //
    //repliedBy should be a subcollection for every message which should contain documents with:
    //from - id of the user that replied to the message
    //text - message from the user that replied to the message

    Task DisplayMessages()
    {
        return firestore.Collection("messages").GetSnapshotAsync().ContinueWithOnMainThread(task => { 
            QuerySnapshot allMessagesSnapshot = task.Result;

            foreach (DocumentSnapshot documentSnapshot in allMessagesSnapshot.Documents) 
            {
                //Query the documents inside the repliedBy subcollection

                //messageIds.Add(documentSnapshot.Id);
                Debug.Log("Message id: " + documentSnapshot.Id);

                string name = "";
                string textMessage = "";
                string timestamp = "";
                string messageFromId = "";

                Dictionary<string, object> message = new Dictionary<string, object>();
                message = documentSnapshot.ToDictionary();

                foreach(KeyValuePair<string, object> pair in message) 
                {
                    Debug.Log($"{pair.Key}: {pair.Value}");

                    if(pair.Key == "name")
                    {
                        name = pair.Value.ToString();
                    }
                    else if(pair.Key == "text") 
                    { 
                        textMessage = pair.Value.ToString();
                    }
                    else if(pair.Key == "timestamp")
                    {
                        timestamp = pair.Value.ToString();
                    }
                    else if(pair.Key == "from")
                    {
                        messageFromId = pair.Value.ToString();
                    }
                }

                Debug.Log("");

                //If the message is sent by the user or it has been replied to, it shouldn't be listed to new messages
                if (name != "" && textMessage != "" && timestamp != "" && messageFromId != userId)
                {
                    timestamp = timestamp.Substring(11, 10) + " " + timestamp.Substring(22, 8);

                    /*object messageToAdd = new
                    {
                        name = name,
                        textMessage = textMessage,
                        timestamp = timestamp
                    };*/
                    messages.Add(new Message(name, textMessage, timestamp));

                    Debug.Log($"New messages: {message.Count}");

                   /* Text textUI = Instantiate(textPrefab, chatMessagesUI.transform);
                    textUI.text = textMessage + "\n" + name + "\n" + timestamp;*/
                } 
            }
        });
    }

    void DisplayMessagesListener()
    {
        Query query = firestore.Collection("messages");

        ListenerRegistration listener = query.Listen(snapshot => {

            foreach(DocumentSnapshot documentSnapshot in snapshot.Documents) 
            {
                if (messageIds.Contains(documentSnapshot.Id) == false)
                {
                    Debug.Log("Entries on listener ");
                    Debug.Log("Message id: " + documentSnapshot.Id);

                    string name = "";
                    string textMessage = "";
                    string timestamp = "";

                    Dictionary<string, object> message = new Dictionary<string, object>();
                    message = documentSnapshot.ToDictionary();

                    foreach (KeyValuePair<string, object> pair in message)
                    {
                        Debug.Log($"{pair.Key}: {pair.Value}");

                        if (pair.Key == "name")
                        {
                            name = pair.Value.ToString();
                        }
                        else if (pair.Key == "text")
                        {
                            textMessage = pair.Value.ToString();
                        }
                        else if (pair.Key == "timestamp")
                        {
                            timestamp = pair.Value.ToString();
                        }

                    }

                    Debug.Log("");

                    if (name != "" && textMessage != "" && timestamp != "")
                    {
                        timestamp = timestamp.Substring(11, 10) + " " + timestamp.Substring(22, 8);

                        //Text textUI = Instantiate(textPrefab, chatMessagesUI.transform);
                        //textUI.text = textMessage + "\n" + name + "\n" + timestamp;
                    }

                    messageIds.Add(documentSnapshot.Id);
                }
            }
        });
    }

    void SendMessage(Text textUI)
    {
        List<string> allUserIds = new List<string>();

        //query to get all the user ids of existing users
        Query allUserIdsQuery = firestore.Collection("users");
        allUserIdsQuery.GetSnapshotAsync().ContinueWith(task => 
        { 
            QuerySnapshot allUserIdsQuerySnapshot = task.Result;
            foreach(DocumentSnapshot documentSnapshot in allUserIdsQuerySnapshot) 
            {
                if(documentSnapshot.Id != userId) 
                {
                    allUserIds.Add(documentSnapshot.Id);
                }
            }

            //query to get the name of the user that sent the message
            firestore.Collection("users").Document(userId).GetSnapshotAsync().ContinueWith(task =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists == true)
                {
                    Dictionary<string, object> user = snapshot.ToDictionary();
                    foreach (KeyValuePair<string, object> pair in user)
                    {
                        Debug.Log($"{pair.Key}: {pair.Value}");
                        if (pair.Key == "Name")
                        {
                            userName = (string)pair.Value;

                            var message = new
                            {
                                name = userName,
                                text = textUI.text,
                                timestamp = FieldValue.ServerTimestamp,
                                from = userId,
                                to = allUserIds,
                                repliedBy = ""
                            };

                            firestore.Collection("messages").AddAsync(message).ContinueWith(task =>
                            {
                                Debug.Log($"Newly generated message id is {task.Result.Id}");
                            });

                            break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Document doesnt exist!");
                }
            });
        });
    }

    public void SendMessageOnClick(Text textUI)
    {
        SendMessage(textUI);
    }

    //This should be used in Start()
    async Task WaitAndCreateUIMessages()
    {
        await DisplayMessages();
        textMessagesCounter.text = $"You have {messages.Count} new messages";

        //Create prefabs of text message and text message name for each message in the messages array
        //Enable the first set of prefabs and disable the others at the beginning
        //Enable the other sets of prefabs one by one once the player clicks `Antwoord`
        //Once a message has been answered to, delete from array

        //Updating the list that contains the sets of prefabs
        for (int i = 0; i < messages.Count; i++)
        {
            if(i == 0)
            {
                //Instantiate first set of prefabs and make active
                GameObject textMessage = Instantiate(textMessagePrefab, messageBox.transform);
                GameObject textMessageName = Instantiate(textMessageNamePrefab, messageBox.transform);

                //Set the text
                TextMeshProUGUI textMessageUI = textMessage.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI textMessageNameUI= textMessageName.GetComponent<TextMeshProUGUI>();
                textMessageUI.text = messages[i].TextMessage;
                textMessageNameUI.text = $"Groetjes, \n {messages[i].Name}";

                //Add to list
                setsOfMessages.Add((textMessage, textMessageName));
            }
            else
            {
                //Instantiate the remaining sets of prefabs and disable
                GameObject textMessage = Instantiate(textMessagePrefab, messageBox.transform);
                GameObject textMessageName = Instantiate(textMessageNamePrefab, messageBox.transform);

                //Set the text
                TextMeshProUGUI textMessageUI = textMessage.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI textMessageNameUI = textMessageName.GetComponent<TextMeshProUGUI>();
                textMessageUI.text = messages[i].TextMessage;
                textMessageNameUI.text = $"Groetjes, \n {messages[i].Name}";

                //Disable them and add to list
                textMessage.SetActive(false);
                textMessageName.SetActive(false);
                setsOfMessages.Add((textMessage, textMessageName));
            }
        }
    }

    public void ReplyToMessageOnClick()
    {
        //SEND REPLY

        //Remove the text message prefabs from the scene
        (GameObject textMessageToRemove, GameObject textMessageNameToRemove) = setsOfMessages[0];
        Destroy(textMessageToRemove);
        Destroy(textMessageNameToRemove);

        //Remove the first set of messages and enable the next ones if they exist or close the screen
        setsOfMessages.RemoveAt(0);
        messages.RemoveAt(0);

        if(setsOfMessages.Count> 0) 
        {
            (GameObject textMessage, GameObject textMessageName) = setsOfMessages[0];
            textMessage.SetActive(true);
            textMessageName.SetActive(true);
        }
        else
        {
            panelResponse.SetActive(false);
        }
    }

    // Start is called before the first frame update
    async void Start()
    {
        userHandling = gameObject.GetComponent<UserHandling>();
        firestore = FirebaseFirestore.DefaultInstance;
        userId = userHandling.GetIdFromFile(filePath);

        await WaitAndCreateUIMessages();
        //textMessagesCounter.text = $"You have {messages.Count} new messages";
        Debug.Log($"Messages array size is: {messages.Count}");
        foreach (var message in messages)
        {
            Debug.Log($"Message name is: {message.Name}");
            Debug.Log($"Message text is: {message.TextMessage}");
            Debug.Log($"Message timestamp is: {message.Timestamp}");
        }

    }

    // Update is called once per frame
    void Update()
    {
        textMessagesCounter.text = $"You have {messages.Count} new messages";
    }
}

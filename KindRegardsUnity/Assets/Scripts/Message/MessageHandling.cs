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
    [SerializeField]
    private GameObject messageObject;

    private Animator messageAnimator;
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
    //When sending a message, it should be sent to only a number of people, not to everyone
    //and every player should receive a max of 3 message a day
    //
    //Every user should have these fields in the database
    //lastTimeMessageReceived - indicates the DateTime when the player received his last message
    //messagesReceivedPerDay - the number of messages a player received within a day; maximum is 3
    //
    //New messages are messages that haven't been replied to by a player yet
    //and they should be able to be seen everytime a player goes to mailbox
    //
    //repliedBy should be a subcollection for every message which should contain documents with:
    //from - id of the user that replied to the message
    //text - message from the user that replied to the message

    Task DisplayMessages()
    {
        return firestore.Collection("messages").GetSnapshotAsync().ContinueWithOnMainThread(task => 
        { 
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
                    //Debug.Log($"{pair.Key}: {pair.Value}");

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

                //Debug.Log("");

                //If the message is sent by the user or it has been replied to, it shouldn't be listed to new messages
                if (name != "" && textMessage != "" && timestamp != "" && messageFromId != userId)
                {
                    timestamp = timestamp.Substring(11, 10) + " " + timestamp.Substring(22, 8);
                    DateTime dateTimeTimeStamp = DateTime.Parse(timestamp);
                    //Add two hours because of UTC+2
                    dateTimeTimeStamp = dateTimeTimeStamp.AddHours(2);

                    messages.Add(new Message(name, textMessage, dateTimeTimeStamp));

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

    Task GetActiveUsers(List<(string, string)> allUserIds, List<UserWithMessageInfo> activeUsers)
    {
        //query to get all the user ids of existing users
        Query allUserIdsQuery = firestore.Collection("users");
        return allUserIdsQuery.GetSnapshotAsync().ContinueWith(task =>
        {
            QuerySnapshot allUserIdsQuerySnapshot = task.Result;
            foreach (DocumentSnapshot documentSnapshot in allUserIdsQuerySnapshot)
            {
                if (documentSnapshot.Id != userId)
                {
                    string name = "";
                    bool isActive = false;
                    string lastTimeLoggedIn = "";
                    string lastTimeMessageReceived = "";
                    string messagesReceivedPerDay = "";

                    Dictionary<string, object> user = documentSnapshot.ToDictionary();
                    foreach (KeyValuePair<string, object> pair in user)
                    {
                        if (pair.Key == "Name")
                        {
                            name = pair.Value.ToString();
                            Debug.Log($"User name found: {name}");
                        }
                        else if (pair.Key == "isActive")
                        {
                            isActive = (bool)pair.Value;
                        }
                        else if (pair.Key == "lastTimeLoggedIn")
                        {
                            lastTimeLoggedIn = pair.Value.ToString();
                        }
                        else if (pair.Key == "lastTimeMessageReceived")
                        {
                            lastTimeMessageReceived = pair.Value.ToString();
                        }
                        else if (pair.Key == "messagesReceivedPerDay")
                        {
                            messagesReceivedPerDay = pair.Value.ToString();
                        }
                    }

                    if (name != "" && lastTimeLoggedIn != "" && lastTimeMessageReceived != "" && messagesReceivedPerDay != "")
                    {
                        lastTimeLoggedIn = lastTimeLoggedIn.Substring(11, 10) + " " + lastTimeLoggedIn.Substring(22, 8);
                        lastTimeMessageReceived = lastTimeMessageReceived.Substring(11, 10) + " " + lastTimeMessageReceived.Substring(22, 8);

                        UserWithMessageInfo userWithMessageInfo = new UserWithMessageInfo(name, isActive, DateTime.Parse(lastTimeLoggedIn), DateTime.Parse(lastTimeMessageReceived), int.Parse(messagesReceivedPerDay));

                        //Add 2 hours because DateTime in db is UTC+2
                        userWithMessageInfo.LastTimeLoggedIn = userWithMessageInfo.LastTimeLoggedIn.AddHours(2);
                        userWithMessageInfo.LastTimeMessageReceived = userWithMessageInfo.LastTimeMessageReceived.AddHours(2);

                        //If the last message was received a day or more ago, reset the messagesReceivedPerDay to 0
                        DateTime currentDateTime = DateTime.Now;
                        if(currentDateTime.Date > userWithMessageInfo.LastTimeMessageReceived.Date)
                        {
                            Debug.Log($"Messages received per day for {userWithMessageInfo.Name} is reset to 0!");
                            userWithMessageInfo.MessagesReceivedPerDay = 0;
                        }

                        //Check if players became active or inactive
                        TimeSpan activeTimeDifference = currentDateTime - userWithMessageInfo.LastTimeLoggedIn;
                        double hoursDifference = Math.Abs(activeTimeDifference.TotalHours);
                        Debug.Log($"Activity hour difference between current time and last time logged in is: {hoursDifference}");

                        //Update player activity in db
                        if(hoursDifference < 48 && userWithMessageInfo.IsActive == false) 
                        {
                            userWithMessageInfo.IsActive = true;

                            Dictionary<string, object> update = new Dictionary<string, object>()
                            {
                                {"isActive", userWithMessageInfo.IsActive}
                            };

                            firestore.Collection("users").Document(documentSnapshot.Id).SetAsync(update, SetOptions.MergeAll).ContinueWith((task) =>
                            {
                                Debug.Log($"Updated user is active data for {userWithMessageInfo.Name}!");
                            });
                        }
                        else if(hoursDifference >= 48 && userWithMessageInfo.IsActive == true)
                        {
                            userWithMessageInfo.IsActive = false;

                            Dictionary<string, object> update = new Dictionary<string, object>()
                            {
                                {"isActive", userWithMessageInfo.IsActive}
                            };

                            firestore.Collection("users").Document(documentSnapshot.Id).SetAsync(update, SetOptions.MergeAll).ContinueWith((task) =>
                            {
                                Debug.Log($"Updated user is active data for {userWithMessageInfo.Name}!");
                            });
                        }

                        if(userWithMessageInfo.IsActive == true) 
                        {
                            activeUsers.Add(userWithMessageInfo);
                            //Adding id, userName to distinguish users later
                            allUserIds.Add((documentSnapshot.Id, userWithMessageInfo.Name));
                        }
                    }
                }
            }
        });
    }

    async Task<string> GetUserNameOfSender(string id)
    {
        string userName = "";

        await firestore.Collection("users").Document(id).GetSnapshotAsync().ContinueWith((task) =>
        {
            DocumentSnapshot documentSnapshot = task.Result;
            if(documentSnapshot.Exists == true)
            {
                Dictionary<string, object> user = documentSnapshot.ToDictionary();
                foreach(KeyValuePair<string, object> pair in user) 
                {
                    if(pair.Key == "Name")
                    {
                        userName = pair.Value.ToString();
                    }
                }
            }
        });

        return userName;
    }

    public async Task<bool> SendMessage()
    {
        //Send a message to 33% percent of the active players
        //Prioritize players with less messages received than others

        string userNameOfSender = "";
        List<(string, string)> allUserIds = new List<(string, string)>();
        List<UserWithMessageInfo> activeUsers = new List<UserWithMessageInfo>();

        await GetActiveUsers(allUserIds, activeUsers);

        for (int i = 0; i < allUserIds.Count; i++)
        {
            (string id, string name) = allUserIds[i];
            Debug.Log($"Active user id: {id}");
            Debug.Log($"Active user name: {name}");
        }

        //Getting the name of the user that sent the message
        userNameOfSender = await GetUserNameOfSender(userId);

        //Algorithm to send message to players

        //Message should be sent to 33.3% percent of active players
        int messagesToSend = (int)Math.Floor(activeUsers.Count * 33.3 / 100);
        Debug.Log($"Messages to send: {messagesToSend}");

        //If the value is lower than 1, it should be at least sent to one player
        if (messagesToSend < 1)
        {
            messagesToSend = 1;
            Debug.Log($"Messages to send was lower than 1 and is now: {messagesToSend}");
        }

        int messagesSent = 0;
        int maxMessagesReceived = 3;
        int leastMessagesReceived = maxMessagesReceived;
        List<(string, UserWithMessageInfo)> usersToSendMessagesTo = new List<(string, UserWithMessageInfo)>();

        //Getting the value of the user with the least messages received
        foreach (var userWithMessageInfo in activeUsers)
        {
            if (userWithMessageInfo.MessagesReceivedPerDay < leastMessagesReceived)
            {
                leastMessagesReceived = userWithMessageInfo.MessagesReceivedPerDay;
            }
        }

        //Only send message to players if there are players with 2 or less messages received
        while (messagesSent < messagesToSend)
        {
            if (leastMessagesReceived < maxMessagesReceived)
            {
                foreach (var userWithMessageInfo in activeUsers)
                {
                    bool userFound = false;

                    if (userWithMessageInfo.MessagesReceivedPerDay == leastMessagesReceived)
                    {
                        //add id to user from allUserIds
                        for (int i = 0; i < allUserIds.Count; i++)
                        {
                            (string userId, string nameOfUser) = allUserIds[i];
                            if (nameOfUser.Equals(userWithMessageInfo.Name) == true && usersToSendMessagesTo.Contains((userId, userWithMessageInfo)) == false)
                            {
                                usersToSendMessagesTo.Add((userId, userWithMessageInfo));
                                messagesSent++;
                                userFound = true;
                                break;
                            }
                        }
                    }

                    if(userFound == true)
                    {
                        break;
                    }
                }

                //If the value is lower than 3, it will keep adding until it finds something or break
                leastMessagesReceived++;
            }
            else
            {
                break;
            }
        }

        for(int i = 0; i < usersToSendMessagesTo.Count; i++)
        {
            (string userId, UserWithMessageInfo userWithMessageInfo) = usersToSendMessagesTo[i];
            Debug.Log($"User to send message to: {userWithMessageInfo.Name}");
        }

        //Making a list of userIds of users that receive a message
        List<string> userIdsToSendMessagesTo = new List<string>();
        for (int i = 0; i < usersToSendMessagesTo.Count; i++)
        {
            (string userId, UserWithMessageInfo userToSendMessageTo) = usersToSendMessagesTo[i];

            //Updating the number of messages received per userToSendMessageTo and the time when they received the last message
            userToSendMessageTo.MessagesReceivedPerDay++;
            userToSendMessageTo.LastTimeMessageReceivedTimeStamp = FieldValue.ServerTimestamp;

            userIdsToSendMessagesTo.Add(userId);
        }

        //Updating the users that receive a message in database
        for(int i = 0; i < usersToSendMessagesTo.Count; i++)
        {
            (string userId, UserWithMessageInfo userToSendMessageTo) = usersToSendMessagesTo[i];
            Dictionary<string, object> updates = new Dictionary<string, object> 
            {
                {"lastTimeMessageReceived", userToSendMessageTo.LastTimeMessageReceivedTimeStamp}, 
                {"messagesReceivedPerDay", userToSendMessageTo.MessagesReceivedPerDay}
            };

            await firestore.Collection("users").Document(userId).SetAsync(updates, SetOptions.MergeAll).ContinueWith((task) => 
            {
                Debug.Log($"Updated user message data!");
            });
        }
        
        if(userIdsToSendMessagesTo.Count > 0) 
        {
            var message = new
            {
                name = userNameOfSender,
                text = "Let's see if it works",
                timestamp = FieldValue.ServerTimestamp,
                from = userId,
                to = userIdsToSendMessagesTo,
                repliedBy = ""
            };

            await firestore.Collection("messages").AddAsync(message).ContinueWith(task =>
            {
                Debug.Log($"Newly generated message id is {task.Result.Id}");
            });

            //Playing animation for sending a message
            messageObject.SetActive(true);
            messageAnimator.SetTrigger("MessageCreated");

            return true;
        }
        else
        {
            Debug.Log("There are no users to send messages to!");

            return false;
        }
        
    }

    public async void SendMessageOnClick()
    {
        await SendMessage();
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
        messageAnimator = messageObject.GetComponent<Animator>();

        await WaitAndCreateUIMessages();
        //textMessagesCounter.text = $"You have {messages.Count} new messages";
        Debug.Log($"Messages array size is: {messages.Count}");
        foreach (var message in messages)
        {
            Debug.Log($"Message name is: {message.Name}");
            Debug.Log($"Message text is: {message.TextMessage}");

            //DateTime timestamp = DateTime.Parse(message.Timestamp);
            Debug.Log($"Message timestamp is: {message.Timestamp}");
        }

    }

    // Update is called once per frame
    void Update()
    {
        textMessagesCounter.text = $"You have {messages.Count} new messages";
    }
}

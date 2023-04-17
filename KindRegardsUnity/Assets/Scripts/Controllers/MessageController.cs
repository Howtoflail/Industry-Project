using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    private List<MessageDTO> messages; // A player's personal inbox
    public List<WhitelistItemDTO> whitelist; // Pre-defined list of texts that a user can use to create a message
    public string messageText;

    // For rendering list of whitelist items in the UI
    GameObject whitelistItemObj;
    GameObject sendMessageObj;
    GameObject scrollAreaObj;
    GameObject dialogObj;
    GameObject dialogTextObj;
    GameObject uiCanvasObj;
    GameObject whitelistObj;
    public GameObject regardsTextObj;
    Button btn;
    GameObject contentObj;
    GameObject whitelistItemTextObj;

    private UIController uiController;
    private CameraController cameraController;
    private AnimationController animationController;

    /// <summary>
    /// On application start, do: set pre-defined responses
    /// </summary>
    async void Start()
    {
        uiCanvasObj = GameObject.Find("UI Canvas");
        sendMessageObj = uiCanvasObj.transform.Find("SendMessage").gameObject;
        scrollAreaObj = sendMessageObj.transform.Find("ScrollArea").gameObject;

        whitelistObj = scrollAreaObj.transform.Find("Whitelist").gameObject;
        regardsTextObj = scrollAreaObj.transform.Find("RegardsText").gameObject;
        dialogObj = sendMessageObj.transform.Find("Dialog").gameObject;
        dialogTextObj = dialogObj.transform.Find("DialogText").gameObject;
        contentObj = whitelistObj.transform.Find("Content").gameObject;
        whitelistItemObj = contentObj.transform.Find("WhitelistItem").gameObject;
        btn = whitelistItemObj.GetComponent<Button>();
        whitelistItemTextObj = whitelistItemObj.transform.Find("WhitelistItemText").gameObject;

        uiController = Resources.FindObjectsOfTypeAll<UIController>()[0];
        cameraController = Resources.FindObjectsOfTypeAll<CameraController>()[0];
        animationController = Resources.FindObjectsOfTypeAll<AnimationController>()[0];

        whitelist = await GetWhitelist();
        SetWhitelist();
    }

    /// <summary>
    /// Check the player's inbox for any replies
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Get list of approved sentences to use when creating a message (paper plane)
    /// </summary>
    private async Task<List<WhitelistItemDTO>> GetWhitelist()
    {
        
        var request = UnityWebRequest.Get(APIUrl.CreateV1("/whitelist"));
        return await RequestExecutor.Execute<List<WhitelistItemDTO>>(request);
    }

    /// <summary>
    /// Return list of approved sentences to use when creating a message (paper plane)
    /// </summary>
    private void SetWhitelist()
    {
        Debug.Log(GetWhitelist());
        if (whitelist != null)
        {
            btn.interactable = true;

            foreach (var item in whitelist)
            {
                GameObject duplicate = Instantiate(whitelistItemObj, contentObj.transform);
                GameObject duplicateWhitelistItemText = duplicate.transform.Find("WhitelistItemText").gameObject;
                TextMeshProUGUI t = duplicateWhitelistItemText.GetComponent<TMPro.TextMeshProUGUI>();
                t.SetText(item.text);
            }
            whitelistItemObj.SetActive(false);
        }
        else
        {
            TextMeshProUGUI t = whitelistItemTextObj.GetComponent<TMPro.TextMeshProUGUI>();
            t.SetText("Could not connect to the server!");

            btn.interactable = false;
        }

    }

    public void SetMessageText(TextMeshProUGUI component)
    {
        this.messageText = component.text;

        TextMeshProUGUI t = dialogTextObj.GetComponent<TMPro.TextMeshProUGUI>();
        t.SetText("Are you sure you want to send this message?");
    }

    public async void FireSendMessage()
    {
        //MessageDTO m = await SendMessage();

        //if (m != null)
        //{
        // If the message was successfully send, go to the window and watch the paper plane being send
        uiController.Forward(17);
        cameraController.NewPosition(17);
        animationController.PlayFlyingAnimation();
        uiController.Back();
        //}
        //else
        //{
        //    TextMeshProUGUI t = dialogTextObj.GetComponent<TMPro.TextMeshProUGUI>();
        //    t.SetText("Could not send your messsage!");
        //}
    }

    /// <summary>
    /// Create a message (paper plane) and send it randomly other players
    /// </summary>
    public async Task<MessageDTO> SendMessage()
    {
        //GetRandomRespondents();
        
        if (messageText != "")
        {
            MessageDTO message = new MessageDTO { DeviceId = SystemInfo.deviceUniqueIdentifier, Text = messageText };

            var postData = this.ObjectToByteArray(message);

            using (UnityWebRequest request = UnityWebRequest.Put(APIUrl.CreateV1("/messages"), postData))
            {
                request.SetRequestHeader("deviceId", SystemInfo.deviceUniqueIdentifier);
                request.SetRequestHeader("Content-Type", "application/json");
                request.method="POST";


                MessageDTO m =await RequestExecutor.Execute<MessageDTO>(request);
                return m;
            }
        }
        else
        {
            TextMeshProUGUI t = dialogTextObj.GetComponent<TMPro.TextMeshProUGUI>();
            t.SetText("Please choose a message to send!");
            return null;
        }
    }

    /// <summary>
    /// Return a player's inbox
    /// </summary>
    private List<MessageDTO> GetMessages(bool getAll)
    {
        return messages;
    }

    /// <summary>
    /// Take the message (paper plane) received and attach a gift to it, and send it back to the original player
    /// </summary>
    private async void SendGift(int messageId, int stickerId, string color)
    {
        var body = new Dictionary<string, dynamic>()
        {
            {"Id",messageId},
            {"stickerId",stickerId},
            {"color", color}
        };

        var request = UnityWebRequest.Put(APIUrl.CreateV1("/messages"), this.ObjectToByteArray(body));
        var giftS = await RequestExecutor.Execute<MessageDTO>(request);
    }

    /// <summary>
    /// After receiving the gift, the paper plane can be sent one last time, to thank the sender of the gift
    /// </summary>
    private async void ThankSender(int messageId)
    {
        var body = new Dictionary<string, dynamic>()
        {
            {"Id",messageId},
            {"thanked",true }
        };

        var request = UnityWebRequest.Put(APIUrl.CreateV1("/messages"), this.ObjectToByteArray(body));
        var thank = await RequestExecutor.Execute<MessageDTO>(request);
    }

    /// <summary>
    /// Utility method for mapping
    /// </summary>
    private byte[] ObjectToByteArray(object obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }
}

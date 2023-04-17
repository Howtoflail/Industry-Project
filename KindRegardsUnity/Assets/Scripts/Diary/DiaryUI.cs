using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Search;

public class DiaryUI : MonoBehaviour
{
    [Header("Group objects and panels")]
    public GameObject beforeEntryDiaryObject;
    public GameObject beforeEntryDiaryPanel;
    public GameObject insideDiaryObject;
    public GameObject viewPastEntriesDatesPanel;
    public GameObject viewPastEntryPanel;
    public GameObject newEntryPanel;
    public GameObject inDiaryButtons;
    public GameObject emotionWheelParentPanel;

    [Header("Code")]
    public TMP_InputField insertCodeInput;
    public TMP_Text inDiaryDialog;
    public TMP_Text errorDialog;

    [Header("Add entry")]
    public TMP_Text errorDialogAddEntry;
    public TMP_InputField contentEntryInput;

    [Header("View entry")]
    public TMP_Text textContentEntry;

    [Header("Emotions objects")]
    public Emotions emotions;
    //public Slider sliderViewEntry;
    //public Slider sliderNewEntry;
    public TMP_Text newEntryEmotionText;
    public Image newEntryEmotionImage;
    public TMP_Text viewEntryEmotionText;
    public TMP_Text viewEntryOnDateText;
    public Image viewEntryEmotionImage;


    public GameObject handleViewEntry;
    public TMP_Text textEmotionViewEntry;
    public TMP_Text textEmotion;
    public String newEntrySelectedEmotion;
    public string search;
    public TMP_Text search_text;


    [Header("Istantiating buttons")]
    public GameObject entryButtonPrefab;
    public GameObject entriesListParent;

    [Header("Scripts")]
    private Lock diaryLock;
    public EntryManager entryManager;
    public FileManager fileManager;

    public UIState states;
    private UIController uiController;

    public List<GameObject> clonesDates;
    public List<Entry> entries;
  

    private bool isShown = false;
    private bool isNewEntryDialog = false;
    private int count = 0;
    private bool isOpening = true;
    private string pinCode = "";

    /// <summary>
    /// Start diary - needed to put delay every time the player access the diary, start only called the first time because the diary only disabled when closed. enter only the first time
    /// Dissolve error messages (pop-up texts)
    /// </summary>
    private void Update()
    {
        if (isOpening)
        {
            beforeEntryDiaryObject.SetActive(false);
            Invoke("ShowDiary", 4);
            diaryLock = gameObject.AddComponent<Lock>();
            entryManager = gameObject.AddComponent<EntryManager>();
            fileManager = gameObject.AddComponent<FileManager>();
            uiController = Resources.FindObjectsOfTypeAll<UIController>()[0];

            clonesDates = new List<GameObject>();

            diaryLock.SetCode(fileManager.GetCodeFromFile());
            if (diaryLock.GetCode() != "")
                diaryLock.SetIfCodeSet(true);

            entryManager.SetEntries(fileManager.GetEntriesFromFile());

            OpenPreDiary();
            isOpening = false;
        }
        if (isShown)
        {
            if (!isNewEntryDialog)
            {
                if (errorDialog.color.a > 0)
                {
                    var color = errorDialog.color;
                    color.a -= 0.01f;

                    errorDialog.color = color;
                }
                else
                {
                    isShown = false;
                }
            }
            else
            {

                if (errorDialogAddEntry.color.a > 0)
                {
                    var color = errorDialogAddEntry.color;
                    color.a -= 0.01f;

                    errorDialogAddEntry.color = color;
                }
                else
                {
                    isShown = false;
                    isNewEntryDialog = false;
                }
            }
        }
    }

    //show elements on open
    private void ShowDiary()
    {
        beforeEntryDiaryObject.SetActive(true);
        beforeEntryDiaryPanel.SetActive(true);
    }

    //when diary closed, disable the panel
    private void OnDisable()
    {
        beforeEntryDiaryPanel.SetActive(false);
        isOpening = true;
    }

    /// <summary>
    /// On open diary button clicked, show code panel (to create new code or to unlock the diary)
    /// </summary>
    public void OpenPreDiary()
    {
        errorDialog.enabled = false;
        pinCode = "";
        insertCodeInput.text = "";

        if (!diaryLock.GetIfCodeSet())
            inDiaryDialog.text = "Seems like you haven't set a lock code yet...do it now! :)";
        else
            inDiaryDialog.text = "Enter your code to access your diary!";
    }

    /// <summary>
    /// On successful access, in diary object showed
    /// </summary>
    private void OnSuccessfulAccess()
    {
        uiController.Forward(16);
        pinCode = "";
        insertCodeInput.text = "";
    }

    public void CloseEmotionSelection()
    {
        emotionWheelParentPanel.active = false;
    }

    public void OpenEmotionSelection()
    {
        emotionWheelParentPanel.active = true;
    }

    /// <summary>
    /// On create entry button clicked, input checked (error showed if no content). If not, entry added and panel closed
    /// </summary>
    public void AddNewEntryButtonClicked()
    {
        if (contentEntryInput.text == "")
        {
            errorDialogAddEntry.text = "Please fill all the fields!";
            errorDialogAddEntry.enabled = true;
            isShown = true;
            isNewEntryDialog = true;

            //dissolve text
            var color2 = errorDialogAddEntry.color;
            color2.a = 4f;

            errorDialogAddEntry.color = color2;
            return;
        }

        entryManager.AddNewEntry(contentEntryInput.text, newEntryEmotionText.text);

        fileManager.DeleteEntriesFile();
        fileManager.SaveEntriesInBinary(entryManager.GetEntries());

        uiController.Forward(16);
        CleanPanelAddEntry();
    }

    //clean addEntryPanel and close it
    public void CleanPanelAddEntry()
    {
        contentEntryInput.text = "";
        // sliderNewEntry.value = 0;
        // textEmotion.text = "Very sad";
    }

    /// <summary>
    /// On view entries history button clicked, showed list of entries with time reference
    /// </summary>
    public void ViewPastEntriesByDate()
    {
        CleanEntriesPanel();
        
        entries = entryManager.GetEntries();

        foreach (Entry entry in entries)
        {
            GameObject newDate = Instantiate(entryButtonPrefab, entriesListParent.transform);
            newDate.gameObject.name = "EntryDate" + count.ToString();

            TMP_Text preview_text = GameObject.Find("EntryDate" + count.ToString() + "/Preview").GetComponentInChildren<TMP_Text>();
            TMP_Text date_text = GameObject.Find("EntryDate" + count.ToString() + "/Date").GetComponentInChildren<TMP_Text>();
            Image emoji_text = GameObject.Find("EntryDate" + count.ToString()+"/Emoji"). GetComponentInChildren<Image>();

            string dateString = entryManager.GetTime(Convert.ToDateTime(entry.GetDate()));


            string content = "";

            if (entry.GetContent().Length > 14)
                content = entry.GetContent().Substring(0, 23) + "...";
            else
                content = entry.GetContent();

            preview_text.text = content;
            date_text.text = dateString;
            emoji_text.sprite = DetermineEmotion(entry.GetEmotion());
            newDate.GetComponent<Button>().onClick.AddListener(() => ViewPastEntry(entry));
            newDate.GetComponent<Button>().onClick.AddListener(() => uiController.Forward(11));

            clonesDates.Add(newDate);

            count++;
        }
    }

    public void ViewPastEntriesBySearch()
    {
        
        search = search_text.text.Trim();
        //Debug.Log(search);

        //CleanEntriesPanel();

        if (String.IsNullOrWhiteSpace(search))
        {
            ViewPastEntriesByDate();
            return;
        }

        Debug.Log(search);

        CleanEntriesPanel();
        entries = entryManager.GetEntries();

        foreach (Entry entry in entries)
        {
    
            string content = entry.GetContent();
            string searchContentHighlighted;
            
            int index = content.IndexOf(search);
            if (index != -1)
            {
                Debug.Log(count + "/" + entries.Count + " yes");
                int startIndex = Mathf.Max(0, index - 3);
                int endIndex = Mathf.Min(content.Length, index + search.Length + 3);
                string substring = content.Substring(startIndex, endIndex - startIndex);
                searchContentHighlighted = "<b>" + search + "</b>" + substring.Replace(search, "");
                Debug.Log(substring)
    ;
                GameObject newDate = Instantiate(entryButtonPrefab, entriesListParent.transform);
                newDate.gameObject.name = "EntryDate" + count.ToString();

                TMP_Text preview_text = GameObject.Find("EntryDate" + count.ToString() + "/Preview").GetComponentInChildren<TMP_Text>();
                TMP_Text date_text = GameObject.Find("EntryDate" + count.ToString() + "/Date").GetComponentInChildren<TMP_Text>();
                Image emoji_text = GameObject.Find("EntryDate" + count.ToString() + "/Emoji").GetComponentInChildren<Image>();

                string dateString = entryManager.GetTime(Convert.ToDateTime(entry.GetDate()));

                preview_text.text = substring;
                date_text.text = dateString;
                emoji_text.sprite = DetermineEmotion(entry.GetEmotion());
                newDate.GetComponent<Button>().onClick.AddListener(() => ViewPastEntry(entry));
                newDate.GetComponent<Button>().onClick.AddListener(() => uiController.Forward(11));

               
                //clonesDates.Add(newDate);

                count++;
            }
            
        }
    }

    private Sprite DetermineEmotion(string emotion)
    {
        if (emotion.Contains("happy"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitHappy;
            }
            if (emotion.Contains("very"))
            {
                return emotions.veryHappy;
            }
            return emotions.happy;
        }

        if (emotion.Contains("proud"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitProud;
            }
            if (emotion.Contains("very"))
            {
                return emotions.veryProud;
            }
            return emotions.proud;
        }

        if (emotion.Contains("angry"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitAngry;
            }
            if (emotion.Contains("very"))
            {
                return emotions.veryAngry;
            }
            return emotions.angry;
        }

        if (emotion.Contains("annoyed"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitAnnoyed;
            }
            if (emotion.Contains("very"))
            {
                return emotions.veryAnnoyed;
            }
            return emotions.annoyed;
        }

        if (emotion.Contains("sad"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitSad;
            }
            if (emotion.Contains("very"))
            {
                return emotions.verySad;
            }
            return emotions.sad;
        }

        if (emotion.Contains("bored"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitBored;
            }
            if (emotion.Contains("very"))
            {
                return emotions.veryBored;
            }
            return emotions.bored;
        }

        if (emotion.Contains("tired"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitTired;
            }
            if (emotion.Contains("very"))
            {
                return emotions.veryTired;
            }
            return emotions.tired;
        }

        if (emotion.Contains("calm"))
        {
            if (emotion.Contains("a bit"))
            {
                return emotions.aBitCalm;
            }
            if (emotion.Contains("very"))
            {
                return emotions.veryCalm;
            }
            return emotions.calm;
        }
        return emotions.happy;
    }

    /// <summary>
    /// On entry clicked in the entries list, showed the single entry
    /// </summary>
    public void ViewPastEntry(Entry entry)
    {
        textContentEntry.text = entry.GetContent();
        //Debug.Log(entry.GetContent());
        //Debug.Log(entry.GetEmotion());
        DateTime date = DateTime.Parse(entry.GetDate());
        //Debug.Log(date.ToString("ddd d MMM yyyy"));
        viewEntryEmotionText.text = entry.GetEmotion();
        viewEntryOnDateText.text = "On " + date.ToString("ddd d MMM yyyy") + ",";
        viewEntryEmotionImage.sprite = DetermineEmotion(entry.GetEmotion());
    }

    private void CleanEntriesPanel()
    {
        if (clonesDates.Count > 0)
        {
            foreach (GameObject gameObject in clonesDates)
            {
                    Destroy(gameObject);

            }
            clonesDates.Clear();
        }
    }

    /// <summary>
    /// Keyboard used to insert the code. If code is wrong, error showed. Otherwise, unlock successful.
    /// </summary>
    public void KeypadButtonPressed(string number)
    {
        pinCode = pinCode + number;
        insertCodeInput.text = pinCode;
        
        if(pinCode.Length == 4)
        {
            if (!diaryLock.GetIfCodeSet())
            {

                //i can set the code
                diaryLock.SetCode(insertCodeInput.text);
                fileManager.SaveCodeInBinary(insertCodeInput.text);

                diaryLock.SetIfCodeSet(true);
                pinCode = "";
                insertCodeInput.text = "";
                inDiaryDialog.text = "Enter your code to access your diary!";
            }
            else
            {
                if (diaryLock.CodeCorrect(insertCodeInput.text))
                {
                    OnSuccessfulAccess();
                }
                else
                {
                    errorDialog.text = "The entered code is not correct...please retry!";
                    pinCode = "";
                    insertCodeInput.text = "";
                    errorDialog.enabled = true;
                    isShown = true;

                    //dissolve text
                    var color2 = errorDialog.color;
                    color2.a = 3.5f;

                    errorDialog.color = color2;
                }
            }
        }
    }
}

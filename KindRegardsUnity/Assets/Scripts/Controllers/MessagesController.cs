using Assets.Scripts.DTO;
using Assets.Scripts.Fields;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Diagnostics;
using System.Net;
using System.IO;

public class MessagesController : MonoBehaviour
{
    [SerializeField] public GameObject messagingScreen;
    [SerializeField] public GameObject panel;
    [SerializeField] public GameObject panelSente4nceVariable;
    [SerializeField] public TextMeshProUGUI cloudText;

    [SerializeField] public TextMeshProUGUI id1;
    [SerializeField] public TextMeshProUGUI id2;
    [SerializeField] public TextMeshProUGUI id3;
    [SerializeField] public TextMeshProUGUI id4;
    [SerializeField] public TextMeshProUGUI id5;
    [SerializeField] public TextMeshProUGUI id6;
    [SerializeField] public TextMeshProUGUI id7;

    //sentences
    [SerializeField] public GameObject sentence1;
    [SerializeField] public GameObject sentence2;
    [SerializeField] public GameObject sentence3;
    [SerializeField] public GameObject sentence4;
    [SerializeField] public GameObject sentence5;
    [SerializeField] public GameObject sentence6;
    [SerializeField] public GameObject sentence7;

    [SerializeField] public TextMeshProUGUI sentence1Text;
    [SerializeField] public TextMeshProUGUI sentence2Text;
    [SerializeField] public TextMeshProUGUI sentence3Text;
    [SerializeField] public TextMeshProUGUI sentence4Text;
    [SerializeField] public TextMeshProUGUI sentence5Text;
    [SerializeField] public TextMeshProUGUI sentence6Text;
    [SerializeField] public TextMeshProUGUI sentence7Text;

    //variables
    [SerializeField] public GameObject variable1;
    [SerializeField] public GameObject variable2;
    [SerializeField] public GameObject variable3;
    [SerializeField] public GameObject variable4;
    [SerializeField] public GameObject variable5;
    [SerializeField] public GameObject variable6;
    [SerializeField] public GameObject variable7;

    [SerializeField] public TextMeshProUGUI variable1Text;
    [SerializeField] public TextMeshProUGUI variable2Text;
    [SerializeField] public TextMeshProUGUI variable3Text;
    [SerializeField] public TextMeshProUGUI variable4Text;
    [SerializeField] public TextMeshProUGUI variable5Text;
    [SerializeField] public TextMeshProUGUI variable6Text;
    [SerializeField] public TextMeshProUGUI variable7Text;

    [SerializeField] public TextMeshProUGUI chosenText;
    [SerializeField] public GameObject button;
    [SerializeField] public TextMeshProUGUI Textmessage;

    [SerializeField] public GameObject menuBg;
    [SerializeField] public GameObject send;
    [SerializeField] public GameObject messaging;

    private SentenceDTO sentence; 
    // Start is called before the first frame update
    void Start()
    {
        messagingScreen.active = false;
        panel.active = false;
        panelSente4nceVariable.active = false;
        button.active = false;
        menuBg.active = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //Remove this
    /*private void OnMouseDown()
    {
        messagingScreen.active = true;
    }*/

    //Remove this
    public void StartMessage()
    {
        /*UnityEngine.Debug.Log("i get here");
        messagingScreen.active = true;
        menuBg.active = false;
        send.active = false;
        messaging.active = false;*/
    }
    public void CancalMessage()
    {
        messagingScreen.active = false;
        panel.active = false;
        panelSente4nceVariable.active = false;
        button.active = false;
        menuBg.active = true;
        messaging.active = true;
    }

    public void ChooseCategory()
    {
        panel.active = false;
        panelSente4nceVariable.active = false;
        button.active = false;
        cloudText.text = "Waar wil je iets over vertellen?";
    }

    public async void GetCategory(int CategoryID)
    {
        sentence1.active = false;
        sentence2.active = false;
        sentence3.active = false;
        sentence4.active = false;
        sentence5.active = false;
        sentence6.active = false;
        sentence7.active = false;

        //find sentences for the category
        var request = UnityWebRequest.Get(APIUrl.CreateV1("/category/" +CategoryID));
        CategoryDTO  c = await RequestExecutor.Execute<CategoryDTO>(request);
        int nrOfSentences = 0;
        panel.active = true;
        cloudText.text = "Kies uit deze zinnen!";
        foreach(SentenceDTO s in c.sentences)
        {
            nrOfSentences++;
            switch(nrOfSentences)
            {
                case 1:
                    sentence1.active = true;
                    id1.text = Convert.ToString(s.Id);
                    sentence1Text.text = s.begin_text + s.end_text;
                    break;
                case 2:
                    sentence2.active = true;
                    id2.text = Convert.ToString(s.Id);
                    sentence2Text.text = s.begin_text + s.end_text;
                    break;
                case 3:
                    sentence3.active = true;
                    id3.text = Convert.ToString(s.Id);
                    sentence3Text.text = s.begin_text + s.end_text;
                    break;
                case 4:
                    sentence4.active = true;
                    id4.text = Convert.ToString(s.Id);
                    sentence4Text.text = s.begin_text + s.end_text;
                    break;
                case 5:
                    sentence5.active = true;
                    id5.text = Convert.ToString(s.Id);
                    sentence5Text.text = s.begin_text + s.end_text;
                    break;
                case 6:
                    sentence6.active = true;
                    id6.text = Convert.ToString(s.Id);
                    sentence6Text.text = s.begin_text + s.end_text;
                    break;
                case 7:
                    sentence7.active = true;
                    id7.text = Convert.ToString(s.Id);
                    sentence7Text.text = s.begin_text + s.end_text;
                    break;
            }
        }
    }

    public async void ChooseMessage(int nr)
    {
       
        int id = 0;
            switch (nr)
            {
                case 1:
                    id = Convert.ToInt32(id1.text);
                chosenText.text = sentence1Text.text;
                    break;
            case 2:
                id = Convert.ToInt32(id2.text);
                chosenText.text = sentence2Text.text;
                break;
            case 3:
                id = Convert.ToInt32(id3.text);
                chosenText.text = sentence3Text.text;
                break;
            case 4:
                id = Convert.ToInt32(id4.text);
                chosenText.text = sentence4Text.text;
                break;
            case 5:
                id = Convert.ToInt32(id5.text);
                chosenText.text = sentence5Text.text;
                break;
            case 6:
                id = Convert.ToInt32(id6.text);
                chosenText.text = sentence6Text.text;
                break;
            case 7:
                id = Convert.ToInt32(id7.text);
                chosenText.text = sentence7Text.text;
                break;

        }
        


        //Find sentence
        var request = UnityWebRequest.Get(APIUrl.CreateV1("/sentence/" + id));
        SentenceDTO c = await RequestExecutor.Execute<SentenceDTO>(request);
        sentence = c;
            variable1.active = false;
            variable2.active = false;
            variable3.active = false;
            variable4.active = false;
            variable5.active = false;
            variable6.active = false;
            variable7.active = false;
        if(c.variable == false)
        {
            button.active = true;
        }
        else
        {
            int nrOfSentences = 0;
            foreach (VariableDTO s in c.variables)
            {
                nrOfSentences++;
                switch (nrOfSentences)
                {
                    case 1:
                        variable1.active = true;
                        variable1Text.text = s.name;
                        break;
                    case 2:
                        variable2.active = true;
                        variable2Text.text = s.name;
                        break;
                    case 3:
                        variable3.active = true;
                        variable3Text.text = s.name;
                        break;
                    case 4:
                        variable4.active = true;
                        variable4Text.text = s.name;
                        break;
                    case 5:
                        variable5.active = true;
                        variable5Text.text = s.name;
                        break;
                    case 6:
                        variable6.active = true;
                        variable6Text.text = s.name;
                        break;
                    case 7:
                        variable7.active = true;
                        variable7Text.text = s.name;
                        break;
                }
            }
        }
        cloudText.text = "Maak de zin af!";
        panelSente4nceVariable.active = true;
    }

    public void ChooseVariable(int id)
    {
            switch (id)
            {
                case 1:
                    chosenText.text = sentence.begin_text+ variable1Text.text +" "+ sentence.end_text;
                    break;
                case 2:
                    chosenText.text = sentence.begin_text + variable2Text.text + " " + sentence.end_text;
                    break;
                case 3:
                    chosenText.text = sentence.begin_text + variable3Text.text + " " + sentence.end_text;
                    break;
                case 4:
                    chosenText.text = sentence.begin_text + variable4Text.text + " " + sentence.end_text;
                    break;
                case 5:
                    chosenText.text = sentence.begin_text + variable5Text.text + " " + sentence.end_text;
                    break;
                case 6:
                    chosenText.text = sentence.begin_text + variable6Text.text + " " + sentence.end_text;
                    break;
                case 7:
                    chosenText.text = sentence.begin_text + variable7Text.text + " " + sentence.end_text;
                    break;
            }
        button.active = true;
    }

    public void AddSentence()
    {
        Textmessage.text = Textmessage.text + " " + chosenText.text;
        chosenText.text = "";
        ChooseCategory();
        send.active = true;
    }
}

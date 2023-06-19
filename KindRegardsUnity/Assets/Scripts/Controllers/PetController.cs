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

public class PetController : MonoBehaviour
{
    private PetDTO petDTO;
    [SerializeField]
    private UnityEngine.Object[] states;
    private GameObject pets;
    private UIController ui;

	private PetInfo petInfo;
    private User currentUser;
    private PetDTO currentPet;
    private int _petChoise;

    [SerializeField]public GameObject canvas;
    [SerializeField] public TextMeshProUGUI name;
    [SerializeField] public GameObject colourManager;
	
	private GameObject stickerRes;

    void Start()
    {
		stickerRes = Resources.Load<GameObject>("Sticker");
		
		petInfo = GameObject.FindWithTag("PetInfo").GetComponent<PetInfo>();
        pets = GameObject.Find("Pets");
        states = Resources.FindObjectsOfTypeAll(typeof(PetState)); //gets all loaded scripts
        ui = canvas.GetComponent<UIController>();
        currentPet= new PetDTO();
		
		petInfo.onLoaded.AddListener(CheckUser);
    }

	private void CheckUser()
	{
		if (petInfo.petName == null)
		{
			UserNotFound();
		}
		else
		{
			UserFound();
		}
	}

    public int petChoise
    {
        get
        {
            return _petChoise;
        }

        set
        {
            var len = Enum.GetNames(typeof(PetStateEnum)).Length;
            _petChoise = ((value % len + len) % len);
        }
    }

    private void UserNotFound()
    {
        pets.transform.position = new Vector3(0, 0.56f, 0);
        ui.NewUser();
		petChoise = 0;
        states = Resources.FindObjectsOfTypeAll(typeof(PetState));
        ShowPet();
    }
    private void UserFound()
    {                
        //var requestPet = UnityWebRequest.Get(APIUrl.CreateV1("/pet/" + user.Id));
        //PetDTO pet = await RequestExecutor.Execute<PetDTO>(requestPet);
        //PetStateEnum pEnum = (PetStateEnum)pet.petKind;
		//Pet p  = new Pet(pet.Id, pEnum, pet.Name, pet.Colour, pet.userId);
		//User u = new User(user.Id, user.diary_code, p, user.computer_code);
		//currentUser = u;
        Color c = stringToColor(petInfo.petColor);
        colourManager.GetComponent<ColourPicker>().SetActualColour(c);
        ShowPetFinal();
        ui.Back();
    }
    public static Color stringToColor(string colorString)
    {
        try
        {
            string[] colors = colorString.Split(',');
            return new Color(float.Parse(colors[0]), float.Parse(colors[1]), float.Parse(colors[2]));
        }
        catch
        {
            return Color.white;
        }
    }
    public static string colorToString(Color color)
    {
        return color.r + "," + color.g + "," + color.b;
    }

    public void ShowPet()
    {
        foreach (PetState state in states) state.DetectActive((PetStateEnum)petChoise);
    }
    private void ShowPetFinal()
    {
        foreach (PetState state in states) state.DetectActive((PetStateEnum)petChoise);
    }

    public void Next()
    {
		petChoise++;
		ShowPet();
    }

    public void Previous()
    {
		petChoise--;
		ShowPet();
    }


    public void Accept()
    {
		var mgr = GameObject.FindWithTag("FirestoreManager").GetComponent<FirestoreManager>();

		if (mgr.ready == true)
		{
			petInfo.petName = name.text;
			
			var stickers = GameObject.FindWithTag("StickerInfo").GetComponent<StickerInfo>();
			
			for (int i = 0; i < 4; i++)
			{
				stickers.Add(1);
			}
			
			var usr = GameObject.FindWithTag("UserInfo").GetComponent<UserInfo>();
			mgr.SetObject(usr, id => {});
			mgr.SetObject(petInfo, id => {});
		}
    }

    public void SetColorCurrentPet(Color color)
    {
        colourManager.GetComponent<ColourPicker>().SetActualColour(color);
    }
}

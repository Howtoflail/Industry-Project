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
    private UnityEngine.Object[] states;
    private GameObject pets;
    private UIController ui;

    private User currentUser;
    private PetDTO currentPet;

    [SerializeField]public GameObject canvas;
    [SerializeField] public TextMeshProUGUI name;
    [SerializeField] public GameObject colourManager;

    async void Start()
    {
        pets = GameObject.Find("Pets");
        states = Resources.FindObjectsOfTypeAll(typeof(PetState));
        ui = canvas.GetComponent<UIController>();
        CheckIfUserExists();
        currentPet= new PetDTO();
    }

    //Check the db to see if there already is a user with this name 
    private async void CheckIfUserExists()
    {
            var request = UnityWebRequest.Get(APIUrl.CreateV1("/user/l" + Environment.UserName));
            UserDTO user = await RequestExecutor.Execute<UserDTO>(request);
        try
        {
           if(user.Id > 0)
            {
                UserFound(user);
            }
        }
        //If user was not innitialized it will throw a null referenceExpection
        catch(NullReferenceException ex)
        {
            UserNotFound();
        }
    }
    
    private void UserNotFound()
    {
       
        //if not continues here
        pets.transform.position = new Vector3(0, 0.56f, 0);
        ui.NewUser();
        currentPet.petKind = 0;
        states = Resources.FindObjectsOfTypeAll(typeof(PetState));
        ShowPet();
    }
    private async void UserFound(UserDTO user)
    {                
        var requestPet = UnityWebRequest.Get(APIUrl.CreateV1("/pet/" + user.Id));
        PetDTO pet = await RequestExecutor.Execute<PetDTO>(requestPet);
        PetStateEnum pEnum = (PetStateEnum)pet.petKind;
            Pet p  = new Pet(pet.Id, pEnum, pet.Name, pet.Colour, pet.userId);
            User u = new User(user.Id, user.diary_code, p, user.computer_code);
            currentUser = u;
        Color c = stringToColor(pet.Colour);
        colourManager.GetComponent<ColourPicker>().SetActualColour(c);
        ShowPetFinal();
        ui.Back();
    }
    public static Color stringToColor(string colorString)
    {
        try
        {
            string[] colors = colorString.Split(',');
            foreach(string color in colors)
            {
                UnityEngine.Debug.Log(color);
            }
            return new Color(float.Parse(colors[0]), float.Parse(colors[1]), float.Parse(colors[2]), float.Parse(colors[3]));
        }
        catch
        {
            return Color.white;
        }
    }
    public static string colorToString(Color color)
    {
        return color.r + "," + color.g + "," + color.b + "," + color.a;
    }

    private void ShowPet()
    {
        foreach (PetState state in states) state.DetectActive((PetStateEnum)currentPet.petKind);
    }
    private void ShowPetFinal()
    {
        foreach (PetState state in states) state.DetectActive((PetStateEnum)currentUser.pet.petKind);
    }

    public void Next()
    {
        int petID = (int)currentPet.petKind;
        petID++;
        if (petID > states.Length - 3) petID = 0;
        currentPet.petKind = petID;
        ShowPet();
    }

    public void Previous()
    {
        int petID = (int)currentPet.petKind;
        petID--;
        if (petID < 0) petID = 2;
        currentPet.petKind = petID;
        ShowPet();
    }


    public void Accept()
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8080/api/v1/user");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"diary_code\":\"0\"," +
                          "\"computer_code\":\"l"+ Environment.UserName + "\"}";

            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            var person = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(result);
            User u = new User(person.Id, person.diary_code, person.computer_code );  
            UnityEngine.Debug.Log(u.id);
            addPet(u);
          
        }

    }
    private void addPet(User result)
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:8080/api/v1/pet");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";
        Color c = colourManager.GetComponent<ColourPicker>().getColor();
        

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {

            string json = "{" +
                            $"\"userId\": {result.id}," +
                            $"\"petKind\": {(int)currentPet.petKind}," +
                            $"\"colour\": {2}," +
                            $"\"name\": \"{name.text}\"" +
                        "}";
            
            streamWriter.Write(json);
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var resultPet = streamReader.ReadToEnd();
            var pet = Newtonsoft.Json.JsonConvert.DeserializeObject<PetDTO>(resultPet);
            Pet p = new Pet(pet.Id, (PetStateEnum)pet.petKind, pet.Name, pet.Colour);
            User u = new User(result.id,  result.diary_code,p, result.computer_code);
            CheckIfUserExists();
        }
    }

}

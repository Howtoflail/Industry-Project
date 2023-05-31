using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UserHandling : MonoBehaviour
{
    private string filePath = @"User.bin";
    private string userId;

    private FirestoreManager firestoreManager;

    public string GetIdFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            return ReadIdBinary();
        }
        else 
        {
            return "";
        }
                
    }

    private string ReadIdBinary()
    {
        FileStream fileStream = null;
        BinaryFormatter binaryFormatter = null;

        try
        {
            fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            binaryFormatter = new BinaryFormatter();

            return binaryFormatter.Deserialize(fileStream).ToString();
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            return null;
        }
        finally
        {
            fileStream?.Close();
        }
    }

    private void SaveIdInBinary(string id)
    {
        FileStream fileStream = null;
        BinaryFormatter binaryFormatter = null;

        try
        {
            fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            binaryFormatter = new BinaryFormatter();

            binaryFormatter.Serialize(fileStream, id);
        }
        catch(Exception ex) 
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            fileStream?.Close();
        }
    }

    void CreateUserOrLogin(DataExample data)
    {
        /*userId = GetIdFromFile(filePath);
        //query the id found in file to make sure it exists on db or create new one and store it on db
        if (userId == "")
        {
            firestoreManager.SetObject(data, result =>
            {
                Debug.Log($"Newly generated user id is {result}");
                SaveIdInBinary(result);
            });
        }
        else
        {
            //query
            firestoreManager.Get
        }*/

        Debug.Log("User id: " + userId);
    }

    void Start()
    {
        /*firestoreManager = gameObject.GetComponent<FirestoreManager>();
        DataExample data = gameObject.GetComponent<DataExample>();
        data.name = "Martin";

        if (firestoreManager.ready == false)
        {
            firestoreManager.onLoaded.AddListener(() => CreateUserOrLogin(data));
        }
        else
        {
            CreateUserOrLogin(data);
        }*/
        //
        FirebaseFirestore firestore = FirebaseFirestore.DefaultInstance;

        userId = GetIdFromFile(filePath);
        //query the id found in file to make sure it exists on db or create new one and store it on db
        if (userId == "")
        {
            var user = new
            {
                //user name should be retrieved from player input
                Name = "Emanuel"
            };

            firestore.Collection("users").AddAsync(user).ContinueWithOnMainThread(task =>
            {
                Debug.Log($"Newly generated user id is {task.Result.Id}");
                SaveIdInBinary(task.Result.Id);
            });
        }
        else
        {
            firestore.Collection("users").Document(userId).GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists == true)
                {
                    Debug.Log($"Document {snapshot.Id} found");
                }
                else
                {
                    Debug.Log("Document doesnt exist!");
                }
            });
        }

        //Debug.Log("User id: " + userId);
    }

    void Update()
    {
        
    }
}

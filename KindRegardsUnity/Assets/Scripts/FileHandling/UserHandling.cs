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
    private FirebaseFirestore firestore;

    //When a user is first created, the user will have a field lastTimeLoggedIn
    //which will be the DateTime of when the user plays the game for the first time
    //
    //This lastTimeLoggedIn field will be updated everytime a player logs in to the game
    //So when querying the id of the user to find whether the user exists or not
    //the field will be updated with the time he logged in
    //
    //A background Android service will run every 48 hours to check if the lastTimeLoggedIn
    //was less than 48 hours ago
    //If the lastTimeLoggedIn was 48 hours ago or more the user will be marked as inactive in the database

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

    void UpdatePlayersActivity()
    {
        Query allUsersQuery = firestore.Collection("users");
        allUsersQuery.GetSnapshotAsync().ContinueWith(task =>
        {
            QuerySnapshot allUsersQuerySnapshot = task.Result;
            foreach(DocumentSnapshot documentSnapshot in allUsersQuerySnapshot) 
            {
                Dictionary<string, object> user = documentSnapshot.ToDictionary();
                foreach(KeyValuePair<string, object> pair in user)
                {
                    Dictionary<string, object> updates = new Dictionary<string, object>
                        {
                            {"messagesReceivedPerDay", 0},
                            {"lastTimeMessageReceived", FieldValue.ServerTimestamp }
                        };

                    DocumentReference userDocReference = documentSnapshot.Reference;
                    userDocReference.SetAsync(updates, SetOptions.MergeAll).ContinueWith(task =>
                    {
                        Debug.Log("Updates are created!");
                    });
                }
            }
        });
    }

    void Start()
    {
        firestore = FirebaseFirestore.DefaultInstance;

        userId = GetIdFromFile(filePath);

        //Query the id found in file to make sure it exists on db or create new one and store it on db
        if (userId == "")
        {
            //Redirect to character creation

            var user = new
            {
                //user name should be retrieved from player input
                Name = "Emanuel",
                lastTimeLoggedIn = FieldValue.ServerTimestamp,
                isActive = true
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
                    //Redirect to character creation
                    Debug.Log("Document doesnt exist!");
                }
            });
        }
    }

    void Update()
    {
        
    }

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
}

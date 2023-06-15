using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public class UserHandling : MonoBehaviour
{
    private string filePathUser = @"User.bin";
    private string userId;
    private string uniqueId;
    private Task sendMessageTask;

    private FirestoreManager firestoreManager;

    //When a user is first created, the user will have a field lastTimeLoggedIn
    //which will be the DateTime of when the user plays the game for the first time
    //
    //This lastTimeLoggedIn field will be updated everytime a player logs in to the game
    //So when querying the id of the user to find whether the user exists or not
    //the field will be updated with the time he logged in
    //
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

    string ReadIdBinary()
    {
        FileStream fileStream = null;
        BinaryFormatter binaryFormatter = null;

        try
        {
            fileStream = new FileStream(filePathUser, FileMode.Open, FileAccess.Read);
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

    void SaveIdInBinary(string id)
    {
        FileStream fileStream = null;
        BinaryFormatter binaryFormatter = null;

        try
        {
            fileStream = new FileStream(filePathUser, FileMode.OpenOrCreate, FileAccess.Write);
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

    void RemoveFile(string filePath)
    {
        if(File.Exists(filePath)) 
        {
            File.Delete(filePath);
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

    async Task UpdatePlayersActivity()
    {
        FirebaseFirestore firestore = FirebaseFirestore.DefaultInstance;
        Query allUsersQuery = firestore.Collection("users");
        await allUsersQuery.GetSnapshotAsync().ContinueWith(async task =>
        {
            QuerySnapshot allUsersQuerySnapshot = task.Result;
            foreach(DocumentSnapshot documentSnapshot in allUsersQuerySnapshot) 
            {
                Dictionary<string, object> user = documentSnapshot.ToDictionary();
                foreach(KeyValuePair<string, object> pair in user)
                {
                    Dictionary<string, object> updates = new Dictionary<string, object>
                        {
                            {"lastTimeMessageSent", FieldValue.ServerTimestamp}
                        };

                    DocumentReference userDocReference = documentSnapshot.Reference;
                    await userDocReference.SetAsync(updates, SetOptions.MergeAll).ContinueWith(task =>
                    {
                        Debug.Log("Updates are created!");
                    });
                }
            }
        });
    }

    public async Task CheckIfUserExistsFromUserIdOrUniqueId(FirebaseFirestore firestore)
    {
        userId = GetIdFromFile(filePathUser);

        //Getting the unique id of the user based on device
        #if UNITY_IOS
            uniqueId = Device.vendorIdentifier;
        #else
            uniqueId = SystemInfo.deviceUniqueIdentifier;
        #endif
        Debug.Log($"Unique id is: {uniqueId}");

        //Checking if a user exists with the current unique id
        if (userId != "")
        {
            bool searchForUser = false;
            bool userFound = false;
            bool snapshotExists = false;

            await firestore.Collection("users").Document(userId).GetSnapshotAsync().ContinueWithOnMainThread((task) => 
            {
                DocumentSnapshot snapshot = task.Result;
                if (snapshot.Exists == true)
                {
                    snapshotExists = true;
                    Debug.Log($"Document {snapshot.Id} found");
                }
                else
                {
                    //Search for user in all users in case the User.bin file has the wrong id
                    Debug.Log("Document doesn't exist!");
                    searchForUser = true;
                }
            });

            if (snapshotExists == true)
            {
                Dictionary<string, object> updates = new Dictionary<string, object>
                    {
                        {"lastTimeLoggedIn", FieldValue.ServerTimestamp},
                        {"uniqueId", uniqueId}
                    };

                await firestore.Collection("users").Document(userId).SetAsync(updates, SetOptions.MergeAll).ContinueWithOnMainThread((task) =>
                {
                    Debug.Log($"Updated user last time logged in data!");
                });
            }

            if (searchForUser == true)
            {
                await firestore.Collection("users").GetSnapshotAsync().ContinueWithOnMainThread((task) => 
                {
                    QuerySnapshot userSnapshots = task.Result;
                    foreach(DocumentSnapshot documentSnapshot in userSnapshots) 
                    {
                        string uniqueIdFromDb = "";
                        string newUserId = documentSnapshot.Id;
                        string name = "";

                        Dictionary<string, object> user = documentSnapshot.ToDictionary();
                        foreach (KeyValuePair<string, object> pair in user)
                        {
                            if(pair.Key == "Name")
                            {
                                name = pair.Value.ToString();
                            }
                            else if(pair.Key == "uniqueId")
                            {
                                uniqueIdFromDb = pair.Value.ToString();
                                break;
                            }
                        }

                        if(uniqueId.Equals(uniqueIdFromDb))
                        {
                            Debug.Log($"The user id from the User.bin file was incorrect! User name: {name}");

                            //Add the correct user id to the User.bin file
                            userId = newUserId;
                            //Remove the existing file and create the new one with the correct user id
                            RemoveFile(filePathUser);
                            SaveIdInBinary(userId);
                            userFound = true;
                            break;
                        }
                    }
                });

                //If user was not found, redirect to character creation
                if (userFound == false)
                {
                    var user = new
                    {
                        //user name should be retrieved from player input
                        Name = "Kalkstein",
                        isActive = true,
                        lastTimeLoggedIn = FieldValue.ServerTimestamp,
                        lastTimeMessageReceived = FieldValue.ServerTimestamp,
                        messagesReceivedPerDay = 0,
                        uniqueId = uniqueId
                    };

                    await firestore.Collection("users").AddAsync(user).ContinueWithOnMainThread(task =>
                    {
                        Debug.Log($"Newly generated user id is {task.Result.Id}");
                        SaveIdInBinary(task.Result.Id);
                        userId = task.Result.Id;
                    });
                }
                else
                {
                    //If user was found update his last time logged in time
                    Dictionary<string, object> update = new Dictionary<string, object>
                    {
                        {"lastTimeLoggedIn", FieldValue.ServerTimestamp}
                    };

                    await firestore.Collection("users").Document(userId).SetAsync(update, SetOptions.MergeAll).ContinueWithOnMainThread((task) =>
                    {
                        Debug.Log($"Updated user last time logged in data!");
                    });
                }
            }
        }
        else
        {
            //Check if the user already exists based on the unique id
            //If not, prompt to user creation
            bool userFound = false;

            await firestore.Collection("users").GetSnapshotAsync().ContinueWithOnMainThread((task) =>
            {
                QuerySnapshot userSnapshots = task.Result;
                foreach (DocumentSnapshot documentSnapshot in userSnapshots)
                {
                    string uniqueIdFromDb = "";
                    string newUserId = documentSnapshot.Id;
                    string name = "";

                    Dictionary<string, object> user = documentSnapshot.ToDictionary();
                    foreach (KeyValuePair<string, object> pair in user)
                    {
                        if (pair.Key == "Name")
                        {
                            name = pair.Value.ToString();
                        }
                        else if (pair.Key == "uniqueId")
                        {
                            uniqueIdFromDb = pair.Value.ToString();
                            break;
                        }
                    }

                    if (uniqueId.Equals(uniqueIdFromDb))
                    {
                        Debug.Log($"User didn't have User.bin file but was found! User name: {name}");

                        //Add the correct user id to the User.bin file
                        userId = newUserId;
                        SaveIdInBinary(userId);
                        userFound = true;
                        break;
                    }
                }
            });

            //If user was not found, redirect to character creation
            if (userFound == false) 
            {
                var user = new
                {
                    //user name should be retrieved from player input
                    Name = "Kalkstein",
                    isActive = true,
                    lastTimeLoggedIn = FieldValue.ServerTimestamp,
                    lastTimeMessageReceived = FieldValue.ServerTimestamp,
                    messagesReceivedPerDay = 0,
                    uniqueId = uniqueId
                };

                await firestore.Collection("users").AddAsync(user).ContinueWithOnMainThread(task =>
                {
                    Debug.Log($"Newly generated user id is {task.Result.Id}");
                    SaveIdInBinary(task.Result.Id);
                    userId = task.Result.Id;
                });
            }
            else
            {
                //If user was found update his last time logged in time
                Dictionary<string, object> update = new Dictionary<string, object>
                {
                    {"lastTimeLoggedIn", FieldValue.ServerTimestamp}
                };

                await firestore.Collection("users").Document(userId).SetAsync(update, SetOptions.MergeAll).ContinueWithOnMainThread((task) =>
                {
                    Debug.Log($"Updated user last time logged in data!");
                });
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

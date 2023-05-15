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

    private string GetIdFromFile()
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

    void Start()
    {
        userId = GetIdFromFile();
        if(userId == "")
        {
            SaveIdInBinary("New User");
            userId = GetIdFromFile();
        }

        Debug.Log("User id: " + userId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

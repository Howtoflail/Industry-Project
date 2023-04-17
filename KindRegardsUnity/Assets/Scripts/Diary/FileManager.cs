using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class FileManager : MonoBehaviour
{
    //path to get code and entries - to be changed with mobile paths in the future!
    private string pathCode = @"MyCode.bin";
    private string pathEntries = @"MyEntries.bin";

    public string GetCodeFromFile()
    {
        if (File.Exists(pathCode))
            return ReadCodeBinary();
        else
            return "";
    }

    public List<Entry> GetEntriesFromFile()
    {
        if (File.Exists(pathEntries))
            return ReadEntriesBinary();
        else
            return null;
    }

    //read file and save code in a variable
    private string ReadCodeBinary()
    {
        //code to read from binary
        FileStream fs = null;
        BinaryFormatter bf = null;
        try
        {
            fs = new FileStream(pathCode, FileMode.Open, FileAccess.Read);
            bf = new BinaryFormatter();

            return (string)bf.Deserialize(fs);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return null;
        }
        finally
        {
            if (fs != null)
                fs.Close();
        }
    }

    //read file and save entries in a list
    private List<Entry> ReadEntriesBinary()
    {
        //code to read from binary
        FileStream fs = null;
        BinaryFormatter bf = null;
        try
        {
            fs = new FileStream(pathEntries, FileMode.Open, FileAccess.Read);
            bf = new BinaryFormatter();

            List<Entry> entries = (List<Entry>)bf.Deserialize(fs);
            Debug.Log(entries);
            return entries;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return null;
        }
        finally
        {
            if (fs != null)
                fs.Close();
        }
    }

    //code saved in a binary file
    public void SaveCodeInBinary(String code)
    {
        FileStream fs = null;
        BinaryFormatter bf = null;

        try
        {
            fs = new FileStream(pathCode, FileMode.OpenOrCreate, FileAccess.Write); bf = new BinaryFormatter();
            bf.Serialize(fs, code);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            if (fs != null)
                fs.Close();
        }
    }

    //entries saved in a binary file
    public void SaveEntriesInBinary<T>(List<T> list)
    {
        //code to save a list in a binary file
        FileStream fs = null;
        BinaryFormatter bf = null;

        try
        {
            fs = new FileStream(pathEntries, FileMode.OpenOrCreate, FileAccess.Write); bf = new BinaryFormatter();
            bf.Serialize(fs, list);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            if (fs != null)
                fs.Close();
        }
    }

    public void DeleteEntriesFile()
    {
        File.Delete(pathEntries);
    }
}

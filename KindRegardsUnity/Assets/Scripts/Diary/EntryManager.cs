using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

[System.Serializable]
public class EntryManager : MonoBehaviour
{
    private List<Entry> diaryEntries;

    public void AddNewEntry(string content, string emotion)
    {
        Entry entry = new Entry();

        entry.SetContent(content);
        entry.SetDate();
        entry.SetEmotion(emotion);

        diaryEntries.Insert(0, entry);
    }

    public List<Entry> GetEntries()
    {
        return diaryEntries;
    }

    public void SetEntries(List<Entry> loadedEntries)
    {
        if (loadedEntries == null)
            diaryEntries = new List<Entry>();
        else
            diaryEntries = loadedEntries;

    }

    public string SaveEntriesInFile()
    {
        return JsonConvert.SerializeObject(diaryEntries);
    }

    public string GetTime(DateTime entryDate)
    {
        DateTime now = DateTime.Now;
        now = now.Date;
        TimeSpan span = now.Subtract(entryDate);

        if((int)span.TotalDays == 0)
        {
            return "Today";
        }
        else if ((int)span.TotalDays == 1)
        {
            return "Yesterday";
        }
        else if ((int)span.TotalDays == 2)
        {
            return "2 days ago";
        }
        else if ((int)span.TotalDays == 3)
        {
            return "3 days ago";
        }
        else if ((int)span.TotalDays == 4)
        {
            return "4 days ago";
        }
        else if ((int)span.TotalDays == 5)
        {
            return "5 days ago";
        }
        else if ((int)span.TotalDays == 6)
        {
            return "6 days ago";
        }
        else if ((int)span.TotalDays > 7 && (int)span.TotalDays <= 13)
        {
            return "1 Week ago";
        }
        else if ((int)span.TotalDays >= 14 && (int)span.TotalDays < 21)
        {
            return "2 Weeks ago";
        }
        else if ((int)span.TotalDays >= 21 && (int)span.TotalDays < 28)
        {
            return "3 Weeks ago";
        }
        else if ((int)span.TotalDays >= 28 && (int)span.TotalDays < 60)
        {
            return "Last month";
        }
        else if ((int)span.TotalDays >= 60)
        {
            return "More than one month ago";
        }
        return null;
    }
}

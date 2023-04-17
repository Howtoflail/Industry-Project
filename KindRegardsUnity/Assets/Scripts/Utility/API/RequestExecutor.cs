using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using UnityEngine;
using UnityEngine.Networking;

public static class RequestExecutor
{
    public static async Task<TResultDTO> Execute<TResultDTO>(UnityWebRequest request) where TResultDTO : class
    {
        var process = request.SendWebRequest();

        while (!process.isDone)
        {
            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            return null;
        }

        return ResponseToDTO<TResultDTO>(request.downloadHandler.text);
    }

    public static TResultDTO ResponseToDTO<TResultDTO>(string response) where TResultDTO : class
    {
        try
        {
            return JsonConvert.DeserializeObject<TResultDTO>(response);
        }
        catch (Exception e)
        {
            Debug.LogError($"[RequestExecutor] {e.Message}");

            return null;
        }
    }
}

using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using UnityEngine;
using UnityEngine.Networking;

public class StickerController : MonoBehaviour
{
    private List<StickerDTO> stickers = new List<StickerDTO>();
    private List<StickerDTO> unlockedStickers = new List<StickerDTO>();

    void Start()
    {
        this.SetStickers();
    }

    void Update()
    {

    }

    /// <summary>
    /// Gets all stickers from the API.
    /// </summary>
    private async void SetStickers()
    {
        var request = UnityWebRequest.Get(APIUrl.CreateV1("/stickers"));

        request.SetRequestHeader("deviceId", SystemInfo.deviceUniqueIdentifier);

        var stickers = await RequestExecutor.Execute<List<StickerDTO>>(request);

        if (stickers != null)
        {
            this.stickers = stickers;
            this.unlockedStickers = stickers.FindAll(s => s.Unlocked); // Get all unlocked stickers
        }
    }

    private async Task<StickerDTO> SendUnlockStickerRequest(long stickerID, int amount)
    {
        var body = new Dictionary<string, long>()
        {
            {"id", stickerID},
            {"amount", amount}
        };

        var request = UnityWebRequest.Put(
            APIUrl.CreateV1("/stickers/unlock"),
            this.ObjectToByteArray(body)
        );

        request.SetRequestHeader("deviceId", SystemInfo.deviceUniqueIdentifier);
        request.SetRequestHeader("Content-Type", "application/json");

        return await RequestExecutor.Execute<StickerDTO>(request);
    }

    private byte[] ObjectToByteArray(object obj)
    {
        string json = JsonConvert.SerializeObject(obj);
        return System.Text.Encoding.UTF8.GetBytes(json);
    }

    public List<StickerDTO> GetSickers(bool getAll)
    {
        return getAll ? this.stickers : this.unlockedStickers;
    }

    public async void UseSticker(long stickerID)
    {
        var sticker = this.stickers.Find(s => s.ID == stickerID);

        if (sticker == null)
        {
            return;
        }

        if (!sticker.Unlocked)
        {
            return;
        }

        if (sticker.Amount > 0)
        {
            sticker.Amount -= 1;
        }
        else
        {
            sticker.Amount = 0;
        }

        await this.SendUnlockStickerRequest(sticker.ID, sticker.Amount);
    }

    public async void ReceiveSticker(long stickerID)
    {
        var unlockedSticker = await this.SendUnlockStickerRequest(stickerID, 1);

        if (unlockedSticker == null)
        {
            return;
        }

        var stickerInstance = this.stickers.Find(s => s.ID == unlockedSticker.ID);

        if (stickerInstance == null)
        {
            this.stickers.Add(unlockedSticker);
            return;
        }

        stickerInstance.Image = unlockedSticker.Image;
        stickerInstance.Amount = unlockedSticker.Amount;
        stickerInstance.Unlocked = unlockedSticker.Unlocked;
    }
}

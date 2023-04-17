using System;

[Serializable]
public class StickerDTO
{
    public int ID { get; set; }
    public string Image { get; set; }
    public bool Unlocked { get; set; }
    public int Amount { get; set; }
}

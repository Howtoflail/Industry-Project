using System;

[Serializable]
public class MessageDTO
{
    public int Id { get; set; }
    public string DeviceId { get; set; } 
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public GiftDTO Gift { get; set; }
    public bool IsRead { get; set; } = false;
    public bool Thanked { get; set; } = false;
}

using System;

public interface IInventoryItem
{
    public string Id { get; set; }
    public string ItemType { get; }
    public string Type { get; set; }
}

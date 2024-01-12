using Godot;
using Godot.Collections;

public partial class InventoryData : Resource
{
    [Export]
    public Array<ItemData> Items = new();

    public void AddItem(ItemData item)
    {
        Items.Add(item);
    }
}

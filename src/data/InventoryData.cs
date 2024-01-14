using Godot;
using Godot.Collections;

public partial class InventoryData : Resource
{
    [Export]
    public Array<ItemSlotData> ItemSlots = new();

    public void AddSlot()
    {
        ItemSlots.Add(new());
    }
}

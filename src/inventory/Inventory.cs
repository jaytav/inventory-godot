using Godot;
using Godot.Collections;

public partial class Inventory : Resource
{
    [Export]
    public Array<ItemSlot> ItemSlots = new();
}

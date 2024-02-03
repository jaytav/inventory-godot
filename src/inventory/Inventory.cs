using Godot;
using Godot.Collections;

public partial class Inventory : Resource
{
    [Export]
    public Dictionary<Vector2, Item> Items;

    [Export]
    public Dictionary<Vector2, ItemSlot> ItemSlots;
}

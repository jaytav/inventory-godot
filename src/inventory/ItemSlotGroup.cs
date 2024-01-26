using Godot;
using Godot.Collections;

public partial class ItemSlotGroup : Resource
{
    [Export]
    public Dictionary<Vector2, ItemSlot> ItemSlots = new();
}

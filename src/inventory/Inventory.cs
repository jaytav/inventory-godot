using Godot;
using Godot.Collections;

public partial class Inventory : Resource
{
    [Export]
    public Dictionary<Vector2, Item> Items;

    [Export]
    public Dictionary<Vector2, ItemSlot> ItemSlots;

    public void MoveItem(Vector2 from, Vector2 to)
    {
        Item fromItem = Items[from];

        if (Items.ContainsKey(to))
        {
            Item toItem = Items[to];
            toItem.Position = from;
            Items[from] = toItem;
        }
        else
        {
            Items.Remove(from);
        }

        fromItem.Position = to;
        Items[to] = fromItem;

        ResourceSaver.Save(this);
    }
}

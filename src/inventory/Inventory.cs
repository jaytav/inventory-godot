using Godot;
using Godot.Collections;

public partial class Inventory : Resource
{
    [Export]
    public Array<Item> Items = new();

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
}

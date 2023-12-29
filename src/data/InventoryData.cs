using Godot;
using Godot.Collections;

public partial class InventoryData : Resource
{
    [Signal]
    public delegate void ItemAddedEventHandler(ItemData itemData);

    [Signal]
    public delegate void ItemRemovedEventHandler(ItemData itemData);

    [Export]
    public Array<ItemData> Items = new();

    [Export]
    public int Size = 1;

    public void AddItem(ItemData itemData)
    {
        if (Items.Count >= Size)
        {
            GD.PushWarning("Failed to add item, size exceeded");
            return;
        }

        // duplicating itemData prevents premade ItemData properties from updating when making
        // changes to this item. E.g. Quantity updates
        itemData = (ItemData)itemData.Duplicate();
        Items.Add(itemData);
        EmitSignal(nameof(ItemAdded), itemData);
    }

    public void RemoveItem(ItemData itemData)
    {
        Items.Remove(itemData);
        EmitSignal(nameof(ItemRemoved), itemData);
    }
}

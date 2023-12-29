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

    public void AddItem(ItemData itemData)
    {
        int nextAvailableItemSlot = getNextAvailableItemSlot();

        if (nextAvailableItemSlot == -1)
        {
            GD.PushWarning("Failed to add item, no available slots");
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

    private int getNextAvailableItemSlot()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] == null)
            {
                return i;
            }
        }

        return -1;
    }
}

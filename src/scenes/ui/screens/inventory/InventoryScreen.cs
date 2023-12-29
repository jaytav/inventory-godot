using Godot;

public partial class InventoryScreen : Control
{
    [Export]
    public InventoryData InventoryData;

    private PackedScene _inventoryScreenItem = GD.Load<PackedScene>("res://src/scenes/ui/screens/inventory/item/InventoryScreenItem.tscn");

    public override void _Ready()
    {
        if (InventoryData == null)
        {
            GD.PushWarning("Failed to initialise, InventoryData is not set");
            return;
        }

        foreach (ItemData itemData in InventoryData.Items)
        {
            InventoryScreenItem inventoryScreenItem = _inventoryScreenItem.Instantiate<InventoryScreenItem>();
            GetNode("Items").AddChild(inventoryScreenItem);
            inventoryScreenItem.ItemData = itemData;
        }
    }
}

using Godot;

public partial class Inventory : Node2D
{
    [Export]
    public InventoryData InventoryData;

    // Scene nodes
    private Node2D _itemSlots;

    // Packed scenes
    private PackedScene _itemSlot = GD.Load<PackedScene>("res://src/scenes/inventory/ItemSlot.tscn");

    public override void _EnterTree()
    {
        InitialiseSceneNodes();
        SpawnItemSlots();
    }

    private void InitialiseSceneNodes()
    {
        _itemSlots = GetNode<Node2D>("ItemSlots");
    }

    private void SpawnItemSlots()
    {
        foreach (ItemSlotData itemSlotData in InventoryData.ItemSlots)
        {
            ItemSlot itemSlot = _itemSlot.Instantiate<ItemSlot>();
            itemSlot.ItemSlotData = itemSlotData;
            _itemSlots.AddChild(itemSlot);
        }
    }
}

using Godot;

public partial class WorldInventory : Node2D
{
    [Export]
    public Inventory Inventory;

    /**
    * Scene nodes
    */
    private Node2D _items;
    private Node2D _itemSlots;

    private PackedScene _worldItem = GD.Load<PackedScene>("res://src/scenes/world/inventory/WorldItem.tscn");
    private PackedScene _worldItemSlot = GD.Load<PackedScene>("res://src/scenes/world/inventory/WorldItemSlot.tscn");

    public override void _Ready()
    {
        InitialiseSceneNodes();

        foreach (Vector2 itemPosition in Inventory.Items.Keys)
        {
            WorldItem worldItem = _worldItem.Instantiate<WorldItem>();
            worldItem.Position = itemPosition * 64;
            _items.AddChild(worldItem);
        }

        foreach (Vector2 itemSlotPosition in Inventory.ItemSlots.Keys)
        {
            WorldItemSlot worldItemSlot = _worldItemSlot.Instantiate<WorldItemSlot>();
            worldItemSlot.Position = itemSlotPosition * 64;
            _itemSlots.AddChild(worldItemSlot);
        }
    }

    private void InitialiseSceneNodes()
    {
        _items = GetNode<Node2D>("Items");
        _itemSlots = GetNode<Node2D>("ItemSlots");
    }
}

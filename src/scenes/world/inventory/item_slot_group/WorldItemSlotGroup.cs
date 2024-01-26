using Godot;

public partial class WorldItemSlotGroup : Node2D
{
    [Export]
    public ItemSlotGroup ItemSlotGroup = new();

    private PackedScene _worldItemSlot = GD.Load<PackedScene>("res://src/scenes/world/inventory/item_slot/WorldItemSlot.tscn");

    /**
    * Scene nodes
    */
    private Area2D _area;
    private Node2D _itemSlots;

    public override void _EnterTree()
    {
        InitialiseSceneNodes();
    }

    public override void _Ready()
    {
        foreach (Vector2 itemSlotPosition in ItemSlotGroup.ItemSlots.Keys)
        {
            WorldItemSlot worldItemSlot = _worldItemSlot.Instantiate<WorldItemSlot>();
            worldItemSlot.ItemSlot = ItemSlotGroup.ItemSlots[itemSlotPosition];
            worldItemSlot.GlobalPosition = itemSlotPosition * 64;
            _itemSlots.AddChild(worldItemSlot);
        }

        RefreshArea();
    }

    private void Highlight()
    {
        foreach (WorldItemSlot worldItemSlot in _itemSlots.GetChildren())
        {
            worldItemSlot.Highlight();
        }
    }

    private void Unhighlight()
    {
        foreach (WorldItemSlot worldItemSlot in _itemSlots.GetChildren())
        {
            worldItemSlot.Unhighlight();
        }
    }

    private void RefreshArea()
    {
        foreach (WorldItemSlot worldItemSlot in _itemSlots.GetChildren())
        {
            foreach (CollisionShape2D collisionShape in worldItemSlot.GetNode("Area").GetChildren())
            {
                CollisionShape2D duplicatedCollisionShape = (CollisionShape2D)collisionShape.Duplicate();
                duplicatedCollisionShape.Position = worldItemSlot.Position;
                _area.AddChild(duplicatedCollisionShape);
            }
        }
    }

    private void InitialiseSceneNodes()
    {
        _area = GetNode<Area2D>("Area");
        _itemSlots = GetNode<Node2D>("ItemSlots");
    }

    /**
    * Signal receivers
    */
    private void OnAreaMouseEntered()
    {
        Highlight();
    }

    private void OnAreaMouseExited()
    {
        Unhighlight();
    }
}

using Godot;
using Godot.Collections;

public partial class WorldInventory : Node2D
{
    [Export]
    public Inventory Inventory;

    private WorldItem _carriedWorldItem;
    private Array<WorldItem> _hoveredWorldItems = new();
    private Array<WorldItemSlot> _hoveredWorldItemSlots = new();

    private PackedScene _worldItem = GD.Load<PackedScene>("res://src/scenes/world/inventory/WorldItem.tscn");
    private PackedScene _worldItemSlot = GD.Load<PackedScene>("res://src/scenes/world/inventory/WorldItemSlot.tscn");

    /**
    * Scene nodes
    */
    private Node2D _items;
    private Node2D _itemSlots;
    private Area2D _mouseArea;

    public override void _Ready()
    {
        InitialiseSceneNodes();

        foreach (Vector2 itemPosition in Inventory.Items.Keys)
        {
            WorldItem worldItem = _worldItem.Instantiate<WorldItem>();
            worldItem.Item = Inventory.Items[itemPosition];
            worldItem.Item.Position = itemPosition;
            _items.AddChild(worldItem);
        }

        foreach (Vector2 itemSlotPosition in Inventory.ItemSlots.Keys)
        {
            WorldItemSlot worldItemSlot = _worldItemSlot.Instantiate<WorldItemSlot>();
            worldItemSlot.ItemSlot = Inventory.ItemSlots[itemSlotPosition];
            worldItemSlot.ItemSlot.Position = itemSlotPosition;
            _itemSlots.AddChild(worldItemSlot);
        }
    }

    public override void _Process(double delta)
    {
        _mouseArea.GlobalPosition = GetGlobalMousePosition();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ActionPrimary") && _hoveredWorldItems.Count > 0)
        {
            _carriedWorldItem = _hoveredWorldItems[0];
            _carriedWorldItem.FollowMouse = true;
        }
        else if (@event.IsActionReleased("ActionPrimary") && _carriedWorldItem != null)
        {
            if (_hoveredWorldItemSlots.Count > 0)
            {
                Inventory.MoveItem(_carriedWorldItem.Item.Position, _hoveredWorldItemSlots[0].ItemSlot.Position);
            }

            _carriedWorldItem.FollowMouse = false;
            _carriedWorldItem = null;
        }
    }

    private void InitialiseSceneNodes()
    {
        _items = GetNode<Node2D>("Items");
        _itemSlots = GetNode<Node2D>("ItemSlots");
        _mouseArea = GetNode<Area2D>("MouseArea");
        _mouseArea.GlobalPosition = GetGlobalMousePosition();
    }

    /**
    * Signal receivers
    */
    private void OnMouseAreaAreaEntered(Area2D area)
    {
        Node areaParent = area.GetParent();

        if (areaParent is WorldItem)
        {
            foreach (WorldItem hoveredWorldItem in _hoveredWorldItems)
            {
                if (hoveredWorldItem == _carriedWorldItem)
                {
                    continue;
                }

                hoveredWorldItem.Unhighlight();
            }

            WorldItem worldItem = (WorldItem)areaParent;
            _hoveredWorldItems.Insert(0, worldItem);
            worldItem.Highlight();
        }
        else if (areaParent is WorldItemSlot)
        {
            WorldItemSlot worldItemSlot = (WorldItemSlot)areaParent;
            _hoveredWorldItemSlots.Insert(0, worldItemSlot);
        }
    }

    private void OnMouseAreaAreaExited(Area2D area)
    {
        Node areaParent = area.GetParent();

        if (areaParent is WorldItem)
        {
            WorldItem worldItem = (WorldItem)areaParent;
            _hoveredWorldItems.Remove(worldItem);
            worldItem.Unhighlight();

            if (_hoveredWorldItems.Count > 0)
            {
                _hoveredWorldItems[0].Highlight();
            }
        }
        else if (areaParent is WorldItemSlot)
        {
            WorldItemSlot worldItemSlot = (WorldItemSlot)areaParent;
            _hoveredWorldItemSlots.Remove(worldItemSlot);
        }
    }
}

using Godot;
using Godot.Collections;

public partial class WorldInventory : Node2D
{
    [Export]
    public InventoryData InventoryData;

    private PackedScene _worldItem = GD.Load<PackedScene>("res://src/scenes/world/inventory/item/WorldItem.tscn");
    private PackedScene _worldItemSlot = GD.Load<PackedScene>("res://src/scenes/world/inventory/item/WorldItemSlot.tscn");

    private Array<WorldItem> _hoveredWorldItems = new();
    private Array<WorldItem> _selectedWorldItems = new();
    private Array<WorldItemSlot> _hoveredWorldItemSlots = new();
    private Array<WorldItemSlot> _selectedWorldItemSlots = new();

    private Area2D _area;

    public override void _Ready()
    {
        _area = GetNode<Area2D>("Area2D");

        for (int i = 0; i < InventoryData.Items.Count; i++)
        {
            if (InventoryData.Items[i] == null)
            {
                continue;
            }

            WorldItem worldItem = _worldItem.Instantiate<WorldItem>();
            worldItem.ItemData = InventoryData.Items[i];
            GetNode("ItemSlots").GetChild(i).AddChild(worldItem);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        _area.GlobalPosition = GetGlobalMousePosition();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ActionPrimary"))
        {
            if (_hoveredWorldItems.Count == 0)
            {
                return;
            }

            WorldItem selectedWorldItem = _hoveredWorldItems[0];
            _selectedWorldItems.Add(selectedWorldItem);
            selectedWorldItem.ToggleFollow();
        }
        else if (@event.IsActionReleased("ActionPrimary"))
        {
            if (_selectedWorldItems.Count == 0)
            {
                return;
            }

            WorldItem selectedWorldItem = _selectedWorldItems[0];
            selectedWorldItem.ToggleFollow();
            _selectedWorldItems.Clear();

            if (_hoveredWorldItemSlots.Count == 0)
            {
                return;
            }

            WorldItemSlot hoveredWorldItemSlot = _hoveredWorldItemSlots[0];
            WorldItem hoveredWorldItem = hoveredWorldItemSlot.GetNodeOrNull<WorldItem>("WorldItem");

            WorldItemSlot selectedWorldItemSlot = selectedWorldItem.GetParent<WorldItemSlot>();
            selectedWorldItem.Reparent(hoveredWorldItemSlot);

            if (hoveredWorldItem != null)
            {
                hoveredWorldItem.Reparent(selectedWorldItemSlot);
                hoveredWorldItem.Name = "WorldItem";
            }

            selectedWorldItem.Name = "WorldItem";
        }
    }

    private void onArea2DAreaEntered(Area2D area)
    {
        Node areaParent = area.GetParent();

        if (areaParent is WorldItem)
        {
            WorldItem worldItem = area.GetParent<WorldItem>();
            _hoveredWorldItems.Insert(0, worldItem);
            worldItem.ToggleHighlight();
        }
        else if (areaParent is WorldItemSlot)
        {
            WorldItemSlot worldItemSlot = area.GetParent<WorldItemSlot>();
            _hoveredWorldItemSlots.Insert(0, worldItemSlot);
            worldItemSlot.ToggleHighlight();
        }
    }

    private void onArea2DAreaExited(Area2D area)
    {
        Node areaParent = area.GetParent();

        if (areaParent is WorldItem)
        {
            WorldItem worldItem = area.GetParent<WorldItem>();
            _hoveredWorldItems.Remove(worldItem);
            worldItem.ToggleHighlight();
        }
        else if (areaParent is WorldItemSlot)
        {
            WorldItemSlot worldItemSlot = area.GetParent<WorldItemSlot>();
            _hoveredWorldItemSlots.Remove(worldItemSlot);
            worldItemSlot.ToggleHighlight();
        }
    }
}

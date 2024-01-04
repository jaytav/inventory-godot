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
        InventoryData.ItemAdded += onInventoryDataItemAdded;
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

            playSound(1.5f);
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
            WorldItemSlot selectedWorldItemSlot = selectedWorldItem.GetParent<WorldItemSlot>();

            if (hoveredWorldItemSlot == selectedWorldItemSlot)
            {
                return;
            }

            playSound(0.5f);
            InventoryData.MoveItem(selectedWorldItem.ItemData, hoveredWorldItemSlot.GetIndex());
            WorldItem hoveredWorldItem = hoveredWorldItemSlot.GetNodeOrNull<WorldItem>("WorldItem");
            selectedWorldItem.Reparent(hoveredWorldItemSlot);

            if (hoveredWorldItem != null)
            {
                if (hoveredWorldItem.ItemData.ResourceName == selectedWorldItem.ItemData.ResourceName)
                {
                    selectedWorldItem.QueueFree();
                }
                else
                {
                    hoveredWorldItem.Reparent(selectedWorldItemSlot);
                    hoveredWorldItem.Name = "WorldItem";
                }
            }

            selectedWorldItem.Name = "WorldItem";
        }
        else if (@event.IsActionPressed("ActionSecondary"))
        {
            if (_hoveredWorldItems.Count == 0)
            {
                return;
            }

            WorldItem selectedWorldItem = _hoveredWorldItems[0];
            InventoryData.SplitItem(selectedWorldItem.ItemData, 1);
        }
    }

    private void playSound(float pitch)
    {
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").PitchScale = pitch;
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
    }

    private void onInventoryDataItemAdded(ItemData itemData, int itemSlot)
    {
        WorldItem worldItem = _worldItem.Instantiate<WorldItem>();
        worldItem.ItemData = itemData;

        WorldItemSlot worldItemSlot = GetNode("ItemSlots").GetChild<WorldItemSlot>(itemSlot);
        worldItemSlot.AddChild(worldItem);
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
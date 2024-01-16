using Godot;
using Godot.Collections;

public partial class Inventory : Node2D
{
    [Export]
    public InventoryData InventoryData;

    private Array<Item> _hoveredItems = new();
    private Array<ItemSlot> _hoveredItemSlots = new();
    private Array<Item> _selectedItems = new();

    // Scene nodes
    private Node2D _itemSlots;

    // Packed scenes
    private PackedScene _itemSlot = GD.Load<PackedScene>("res://src/scenes/inventory/ItemSlot.tscn");

    public override void _EnterTree()
    {
        InitialiseSceneNodes();
        SpawnItemSlots();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ActionPrimary") && _hoveredItems.Count > 0)
        {
            // Pick up item
            Item hoveredItem = _hoveredItems[0];
            hoveredItem.IsDragging = true;
            _selectedItems.Add(hoveredItem);
        }
        else if (@event.IsActionReleased("ActionPrimary") && _selectedItems.Count > 0)
        {
            // Drop item, move to hovering ItemSlot
            Item selectedItem = _selectedItems[0];
            selectedItem.IsDragging = false;
            _selectedItems.Remove(selectedItem);

            if (_hoveredItemSlots.Count == 0)
            {
                return;
            }

            ItemSlot hoveredItemSlot = _hoveredItemSlots[0];
            ItemSlot selectedItemSlot = selectedItem.GetParent<ItemSlot>();

            if (hoveredItemSlot == selectedItemSlot)
            {
                return;
            }

            Item hoveredItem = hoveredItemSlot.GetNode<Item>("Item");
            hoveredItem.Reparent(selectedItemSlot);
            selectedItem.Reparent(hoveredItemSlot);
            hoveredItem.Name = "Item";
        }
        else if (@event.IsActionPressed("RotateItem") && _selectedItems.Count > 0)
        {
            // Rotate item
            Item selectedItem = _selectedItems[0];
            selectedItem.ItemData.Rotation += 90;
        }
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
            itemSlot.Hovered += OnItemSlotHovered;
            itemSlot.Unhovered += OnItemSlotUnhovered;
            _itemSlots.AddChild(itemSlot);

            Item item = itemSlot.GetNode<Item>("Item");
            item.Hovered += OnItemHovered;
            item.Unhovered += OnItemUnhovered;
        }
    }

    /**
     * Signal receivers
    */
    private void OnItemSlotHovered(ItemSlot itemSlot)
    {
        _hoveredItemSlots.Add(itemSlot);
    }

    private void OnItemSlotUnhovered(ItemSlot itemSlot)
    {
        _hoveredItemSlots.Remove(itemSlot);
    }

    private void OnItemHovered(Item item)
    {
        _hoveredItems.Add(item);
    }

    private void OnItemUnhovered(Item item)
    {
        _hoveredItems.Remove(item);
    }
}

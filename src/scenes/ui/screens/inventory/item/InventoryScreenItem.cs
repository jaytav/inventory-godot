using Godot;

public partial class InventoryScreenItem : Button
{
    public ItemData ItemData
    {
        get { return _itemData; }
        set { setItemData(value); }
    }

    private ItemData _itemData;

    public override void _Ready()
    {
        empty();
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        return true;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        InventoryScreenItem inventoryScreenItem = (InventoryScreenItem)data;
        ItemData inventoryScreenItemItemData = inventoryScreenItem.ItemData;
        inventoryScreenItem.ItemData = ItemData;
        ItemData = inventoryScreenItemItemData;
        ButtonPressed = true;
        GrabFocus();
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (ItemData == null)
        {
            return false;
        }

        TextureRect dragPreview = new();
        dragPreview.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        dragPreview.Texture = ItemData.Icon;
        dragPreview.Size = new(32, 32);
        SetDragPreview(dragPreview);
        return this;
    }

    private void empty()
    {
        FocusMode = FocusModeEnum.None;
        MouseDefaultCursorShape = CursorShape.Arrow;
        GetNode<TextureRect>("Icon").Texture = null;
        GetNode<Label>("Quantity").Text = "";
    }

    private void setItemData(ItemData itemData)
    {
        _itemData = itemData;

        if (ItemData == null)
        {
            empty();
            return;
        }

        FocusMode = FocusModeEnum.All;
        MouseDefaultCursorShape = CursorShape.PointingHand;
        GetNode<TextureRect>("Icon").Texture = ItemData.Icon;
        GetNode<Label>("Quantity").Text = ItemData.Quantity.ToString();
    }
}

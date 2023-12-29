using Godot;

public partial class InventoryScreenItem : Control
{
    public ItemData ItemData
    {
        get { return _itemData; }
        set { setItemData(value); }
    }

    private ItemData _itemData;

    public override void _Ready()
    {
        GetNode<TextureRect>("Icon").Texture = null;
        GetNode<Label>("Quantity").Text = "";
    }

    private void onMouseEntered()
    {
        // do something when mouse enters
    }

    private void onMouseExited()
    {
        // do something when mouse exits
    }

    private void setItemData(ItemData itemData)
    {
        _itemData = itemData;

        if (ItemData == null)
        {
            MouseDefaultCursorShape = CursorShape.Arrow;
            return;
        }

        MouseDefaultCursorShape = CursorShape.PointingHand;
        GetNode<TextureRect>("Icon").Texture = ItemData.Icon;
        GetNode<Label>("Quantity").Text = ItemData.Quantity.ToString();
    }
}

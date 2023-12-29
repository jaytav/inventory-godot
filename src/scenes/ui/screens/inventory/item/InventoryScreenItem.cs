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
        GetNode<TextureRect>("Icon").Texture = null;
        GetNode<Label>("Quantity").Text = "";
        GetNode<Control>("Detail").Hide();
    }

    private void onMouseEntered()
    {
        if (ItemData == null)
        {
            return;
        }

        GetNode<Control>("Detail").Show();
    }

    private void onMouseExited()
    {
        GetNode<Control>("Detail").Hide();
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
        GetNode<Label>("Detail/Name").Text = ItemData.ResourceName;
        GetNode<TextureRect>("Detail/Icon").Texture = ItemData.Icon;
        GetNode<Label>("Detail/Description").Text = ItemData.Description;
    }
}

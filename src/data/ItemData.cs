using Godot;

public partial class ItemData : Resource
{
    [Signal]
    public delegate void QuantityUpdatedEventHandler(ItemData itemData, int quantity);

    [Export]
    public int Quantity
    {
        get { return _quantity; }
        set { setQuantity(value); }
    }

    [Export]
    public Texture2D Icon;

    [Export]
    public string Description;

    private int _quantity = 1;

    private void setQuantity(int quantity)
    {
        if (Quantity == quantity)
        {
            return;
        }

        _quantity = quantity;
        EmitSignal(nameof(QuantityUpdated), this, Quantity);
    }
}

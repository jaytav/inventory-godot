using Godot;

public partial class ItemSlot : Node2D
{
    [Signal]
    public delegate void HoveredEventHandler(ItemSlot itemSlot);

    [Signal]
    public delegate void UnhoveredEventHandler(ItemSlot itemSlot);

    [Export]
    public ItemSlotData ItemSlotData = new();

    // Scene nodes
    private Item _item;

    public override void _EnterTree()
    {
        InitialiseSceneNodes();
        _item.ItemData = ItemSlotData.Item;
        GlobalPosition = ItemSlotData.Position * 64;
    }

    private void InitialiseSceneNodes()
    {
        _item = GetNode<Item>("Item");
    }

    /**
     * Signal receivers
    */
    private void OnAreaMouseEntered()
    {
        EmitSignal(nameof(Hovered), this);
    }

    private void OnAreaMouseExited()
    {
        EmitSignal(nameof(Unhovered), this);
    }
}

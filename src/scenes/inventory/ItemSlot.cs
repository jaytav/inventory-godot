using Godot;

public partial class ItemSlot : Node2D
{
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
}

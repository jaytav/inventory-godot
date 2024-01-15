using Godot;

public partial class Item : Node2D
{
    [Export]
    public ItemData ItemData;

    // Scene nodes
    private Sprite2D _icon;

    public override void _EnterTree()
    {
        InitialiseSceneNodes();
        _icon.Texture = ItemData?.Icon;
    }

    private void InitialiseSceneNodes()
    {
        _icon = GetNode<Sprite2D>("Icon");
    }
}

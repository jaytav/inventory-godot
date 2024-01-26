using Godot;

public partial class WorldItemSlot : Node2D
{
    [Export]
    public ItemSlot ItemSlot = new();

    /**
    * Scene nodes
    */
    private Sprite2D _highlight;

    public override void _EnterTree()
    {
        InitialiseSceneNodes();
    }

    private void InitialiseSceneNodes()
    {
        _highlight = GetNode<Sprite2D>("Highlight");
        _highlight.Visible = false;
    }
}

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

    public void Highlight()
    {
        _highlight.Visible = true;
    }

    public void Unhighlight()
    {
        _highlight.Visible = false;
    }

    private void InitialiseSceneNodes()
    {
        _highlight = GetNode<Sprite2D>("Highlight");
        _highlight.Visible = false;
    }
}

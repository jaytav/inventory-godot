using Godot;

public partial class WorldItem : Node2D
{
    [Export]
    public Item Item;

    /**
    * Scene nodes
    */
    private Sprite2D _sprite;

    public override void _Ready()
    {
        InitialiseSceneNodes();
    }

    public void Highlight()
    {
        _sprite.Modulate = new Color(203/255.0f, 213/255.0f, 225/255.0f);
    }

    public void Unhighlight()
    {
        _sprite.Modulate = new Color(15/255.0f, 23/255.0f, 42/255.0f);
    }

    private void InitialiseSceneNodes()
    {
        _sprite = GetNode<Sprite2D>("Sprite");
    }
}

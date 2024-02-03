using Godot;

public partial class WorldItem : Node2D
{
    [Export]
    public Item Item;

    public bool FollowMouse;

    /**
    * Scene nodes
    */
    private Sprite2D _sprite;

    public override void _Ready()
    {
        InitialiseSceneNodes();
    }

    public override void _Process(double delta)
    {
        if (FollowMouse)
        {
            FollowPosition(GetGlobalMousePosition(), delta);
        }
        else
        {
            FollowPosition(Item.Position * 64, delta);
        }
    }

    public void Highlight()
    {
        _sprite.Modulate = new Color(203/255.0f, 213/255.0f, 225/255.0f);
    }

    public void Unhighlight()
    {
        _sprite.Modulate = new Color(15/255.0f, 23/255.0f, 42/255.0f);
    }

    private void FollowPosition(Vector2 position, double delta)
    {
        Vector2 currentPosition = GlobalPosition;
        Vector2 followPosition = position;

        Vector2 newPosition = new();
        newPosition.X = Mathf.Lerp(currentPosition.X, followPosition.X, 50 * (float)delta);
        newPosition.Y = Mathf.Lerp(currentPosition.Y, followPosition.Y, 50 * (float)delta);
        GlobalPosition = newPosition;
    }

    private void InitialiseSceneNodes()
    {
        _sprite = GetNode<Sprite2D>("Sprite");
    }
}

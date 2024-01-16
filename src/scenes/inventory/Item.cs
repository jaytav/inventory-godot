using Godot;

public partial class Item : Node2D
{
    [Signal]
    public delegate void HoveredEventHandler(Item item);

    [Signal]
    public delegate void UnhoveredEventHandler(Item item);

    [Export]
    public ItemData ItemData;

    public bool IsDragging;

    // Scene nodes
    private Sprite2D _icon;

    public override void _EnterTree()
    {
        InitialiseSceneNodes();
        _icon.Texture = ItemData?.Icon;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 position = IsDragging ? GetGlobalMousePosition() : GetParent<Node2D>().GlobalPosition;
        MoveTowards(position, delta);

        float rotation = ItemData?.Rotation ?? 0;
        RotateTowards(rotation, delta);
    }

    private void InitialiseSceneNodes()
    {
        _icon = GetNode<Sprite2D>("Icon");
    }

    private void MoveTowards(Vector2 position, double delta)
    {
        Vector2 currentPosition = GlobalPosition;

        Vector2 newPosition = new();
        newPosition.X = Mathf.Lerp(currentPosition.X, position.X, 20 * (float)delta);
        newPosition.Y = Mathf.Lerp(currentPosition.Y, position.Y, 20 * (float)delta);
        GlobalPosition = newPosition;
    }

    private void RotateTowards(float rotation, double delta)
    {
        GlobalRotation = Mathf.LerpAngle(GlobalRotation, Mathf.DegToRad(rotation), 0.25f);
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

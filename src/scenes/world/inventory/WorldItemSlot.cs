using Godot;

public partial class WorldItemSlot : Node2D
{
    [Export]
    public ItemSlot ItemSlot;

    public override void _Process(double delta)
    {
        FollowPosition(ItemSlot.Position * 64, delta);
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
}

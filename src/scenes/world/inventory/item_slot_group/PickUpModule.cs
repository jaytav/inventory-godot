using Godot;

public partial class PickUpModule : Node2D
{
    [Export]
    private Node2D _followNode;

    [Export]
    private Area2D _area;

    private bool _isHoveringArea = false;
    private bool _isPickedUp = false;

    public override void _EnterTree()
    {
        _area.MouseEntered += OnAreaMouseEntered;
        _area.MouseExited += OnAreaMouseExited;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_isPickedUp)
        {
            return;
        }

        Vector2 currentPosition = _followNode.GlobalPosition;
        Vector2 towardsPosition = GetGlobalMousePosition();

        Vector2 newPosition = new();
        newPosition.X = Mathf.Lerp(currentPosition.X, towardsPosition.X, 50 * (float)delta);
        newPosition.Y = Mathf.Lerp(currentPosition.Y, towardsPosition.Y, 50 * (float)delta);
        _followNode.GlobalPosition = newPosition;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ActionPrimary") && _isHoveringArea)
        {
            _isPickedUp = true;
        }
        else if (@event.IsActionReleased("ActionPrimary"))
        {
            _isPickedUp = false;
        }
    }

    /**
    * Signal receivers
    */
    private void OnAreaMouseEntered()
    {
        _isHoveringArea = true;
    }

    private void OnAreaMouseExited()
    {
        _isHoveringArea = false;
    }
}

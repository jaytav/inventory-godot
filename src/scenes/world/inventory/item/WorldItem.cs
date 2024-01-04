using Godot;

public partial class WorldItem : Node2D
{
    public ItemData ItemData;

    private bool _followMousePosition;

    public override void _Ready()
    {
        if (ItemData == null)
        {
            return;
        }

        ItemData.QuantityUpdated += onItemDataQuantityUpdated;
        GetNode<Sprite2D>("Icon").Texture = ItemData.Icon;
        GetNode<Label>("UI/Quantity").Text = ItemData.Quantity.ToString();
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 target = GetNode<Node2D>("..").GlobalPosition;

        if (_followMousePosition)
        {
            target = GetGlobalMousePosition();
        }

        moveTowards(target, delta);
    }

    public void ToggleFollow()
    {
        _followMousePosition = !_followMousePosition;
    }

    public void ToggleHighlight()
    {
        AnimationPlayer animationPlayer = GetNode<AnimationPlayer>("Icon/AnimationPlayer");

        if (animationPlayer.IsPlaying())
        {
            animationPlayer.Stop();
        }
        else
        {
            animationPlayer.Play("bounce");
        }
    }

    private void moveTowards(Vector2 to, double delta)
    {
        Vector2 currentPosition = GlobalPosition;

        Vector2 newPosition = new();
        newPosition.X = Mathf.Lerp(currentPosition.X, to.X, 20 * (float)delta);
        newPosition.Y = Mathf.Lerp(currentPosition.Y, to.Y, 20 * (float)delta);
        GlobalPosition = newPosition;
    }

    private void onItemDataQuantityUpdated(ItemData itemData, int quantity)
    {
        GetNode<Label>("UI/Quantity").Text = quantity.ToString();
    }
}

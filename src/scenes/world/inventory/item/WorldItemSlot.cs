using Godot;

public partial class WorldItemSlot : Node2D
{
    public void ToggleHighlight()
    {
        Node2D highlight = GetNode<Node2D>("Highlight");
        highlight.Visible = !highlight.Visible;
    }
}

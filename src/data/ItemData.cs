using Godot;
using Godot.Collections;

public partial class ItemData : Resource
{
    [Export]
    public Texture2D Icon = GD.Load<Texture2D>("res://assets/textures/1x1.svg");

    [Export]
    public Array<Vector2> Cells = new() {
        Vector2.Zero,
    };

    [Export]
    public float Rotation;
}

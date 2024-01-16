using Godot;

public partial class ItemData : Resource
{
    [Export]
    public Texture2D Icon = GD.Load<Texture2D>("res://assets/textures/1x1.svg");

    [Export]
    public float Rotation;
}

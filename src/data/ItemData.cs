using Godot;

public partial class ItemData : Resource
{
    [Export]
    public int Quantity = 1;

    [Export]
    public Texture2D Icon;

    [Export]
    public string Description;
}

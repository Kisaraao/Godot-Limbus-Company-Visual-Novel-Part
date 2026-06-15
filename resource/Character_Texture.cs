using Godot;

[GlobalClass]
public partial class Character_Texture : Resource
{
    [Export] public string emotion;
    [Export] public Texture2D texture;
    [Export] public Vector2 scale = Vector2.Zero;
    [Export] public Vector2 offset = Vector2.Zero;
}
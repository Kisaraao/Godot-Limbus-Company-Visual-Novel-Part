using Godot;

[GlobalClass]
public partial class Character_Texture : Resource
{
    [Export] public string emotion;
    [Export] public Texture2D texture;
    [Export] public Vector2 scale = new Vector2(1, 1);
    [Export] public Vector2 offset = new Vector2(0, 0);
}
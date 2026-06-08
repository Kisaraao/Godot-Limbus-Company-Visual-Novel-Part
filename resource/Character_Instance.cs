using Godot;

[GlobalClass]
public partial class Character_Instance : Resource
{
    [Export] public Character character;
    [Export] public float X_index = 0;
    [Export] public Vector2 offset = new Vector2();
    [Export] public string emotion = "idle";
    [Export] public bool gray = false;
    [Export] public bool black = false;
}

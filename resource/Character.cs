using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Character : Resource
{
    [Export] public string title = "未设定身份";
    [Export] public string name = "未设定名称";
    [Export] public Color name_color = new Color("#e9c99f");
    [Export] public Color badge_color = new Color("#ff0000");
    [Export] public Array<Character_Texture> textures;
    [Export] public Vector2 tex_common_scale = new Vector2(1, 1);
    [Export] public Vector2 tex_common_offset = Vector2.Zero;
    [Export] public Texture2D portrait;
}

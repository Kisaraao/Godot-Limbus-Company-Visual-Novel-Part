using Godot;

[GlobalClass]
public partial class Character_Instance : Resource
{
    [Export] public Character character;
    [Export(PropertyHint.Range, "-15,15,1,or_greater")] public int X_index = 0;
    [Export] public Vector2 offset = Vector2.Zero;
    [Export] public Vector2 offset_scale = Vector2.Zero;
    [Export] public string emotion = "idle";
    [Export] public bool gray = false;
    [Export] public bool black = false;

    [ExportGroup("Fade")]
    [Export] public bool fade_in = false;
    [Export] public float fade_in_duration = 0.2f;
    [Export] public bool fade_out = false;
    [Export] public float fade_out_duration = 0.2f;

    [ExportGroup("Animation")]
    [Export] public bool pos_animate = false;
    [Export] public int begin_pos_index_animate = 0;
    [Export] public int end_pos_index_animate = 0;
    [Export] public float pos_animate_duration = 0.5f;
    [Export] public Tween.EaseType pos_animate_ease_type = Tween.EaseType.In;
    [ExportGroup("Shake")]
    [Export] public bool shake = false;
    [Export] public int shake_strength = 1;
    [Export] public float shake_duration = 0.5f;
}
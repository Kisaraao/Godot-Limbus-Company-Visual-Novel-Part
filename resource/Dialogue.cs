using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Dialogue : Resource
{
    [Export] public Character speaker;
    [Export(PropertyHint.MultilineText)] public string content;
    [Export] public Array<Character_Instance> characters;
    
    [ExportGroup("Extra_Options")]
    [ExportSubgroup("Content")]
    [Export] public HorizontalAlignment content_horizontal_alignment = HorizontalAlignment.Left;
    [Export] public double print_speed = 0.04;
    [Export] public Vector2 offset_content = new Vector2(0, 0);
    [Export] public bool hide_content = false;
    [ExportSubgroup("Speaker")]
    [Export] public string title_mute_speaker = "???";
    [Export] public string name_mute_speaker = "???";
    [Export] public bool mute_speaker = false;
    [Export] public bool hide_speaker = false;
    [ExportSubgroup("Place")]
    [Export] public string name_mute_place = "???";
    [Export] public bool mute_place = false;
    [Export] public bool hide_place = false;
    
    [ExportGroup("Fade")]
    [ExportSubgroup("OnBegin")]
    [Export] public bool fade_begin = false;
    [Export] public float fade_begin_duration = 0.2f;
    [Export] public Color fade_begin_start_color = new Color("#ffffff");
    [Export] public Color fade_begin_target_color = new Color("#000000");
    [ExportSubgroup("OnEnd")]
    [Export] public bool fade_end = false;
    [Export] public float fade_end_duration = 0.2f;
    [Export] public Color fade_end_start_color = new Color("#ffffff");
    [Export] public Color fade_end_target_color = new Color("#000000");

    [ExportGroup("Background")]
    [Export] public Place place;
    [Export] public Vector2 background_scale = new Vector2(0.8f, 0.8f);
    [Export] public Vector2 background_offset = new Vector2(0, 0);

    [ExportGroup("Audio")]
    [Export] public AudioStream voice = null;
    [Export] public AudioStream sound = null;
    [Export] public AudioStream bgm = null;

    [ExportGroup("Camera")]
    [Export] public bool camera_pos_smoothing = false;
    [Export] public float speed_camera_pos_smoothing = 5.0f;
    [Export] public Vector2 camera_pos = new Vector2(0, 0);
    [Export] public bool camera_rotation_smoothing = false;
    [Export] public float speed_camera_rotation_smoothing = 5.0f;
    [Export(PropertyHint.Range, "-360,360,0.5")] public float camera_rotation = 0.0f;
    [Export] public bool camera_zoom_smoothing = false;
    [Export] public float duration_camera_zoom_smoothing = 0.5f;
    [Export] public Vector2 camera_zoom = new Vector2(1, 1);
    [Export] public bool camera_shake = false; 
    [Export(PropertyHint.Range, "1,50,1")] public int camera_shake_strength = 1;
    [Export] public float camera_shake_duration = 1.0f;
}
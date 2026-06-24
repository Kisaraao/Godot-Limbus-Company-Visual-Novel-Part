using Godot;

public partial class PopupHisitory : PopupPanel
{
	[Export] public VBoxContainer record_list;
	[Export] public PackedScene scene_record;
	[Export] public AudioStreamPlayer voice;
	[Export] public Button btn_confirm;
	[Export] public AudioManager audio;
	[Export] public ScrollContainer scroll;
	[Export] public Control body;
	public void add_record(DialogueRecord data)
	{
		Record rc = scene_record.Instantiate<Record>();
		rc.data = data;
		rc.voice = voice;
		rc.audio = audio;
		record_list.AddChild(rc);
	}

	public void _on_confirm_pressed()
	{
		Hide();
	}

	public void _on_confirm_mouse_entered()
	{
		audio.playSound("hover");
		btn_confirm.Modulate = new Color(0.8f, 0.8f, 0.8f);
	}

	public void _on_confirm_mouse_exited()
	{
		btn_confirm.Modulate = Colors.White;
	}

	public void _on_about_to_popup()
	{
		body.OffsetTransformPosition = new Vector2(0, -15);
		var tween = GetTree().CreateTween();
		tween.TweenProperty(body, "offset_transform_position", Vector2.Zero, 0.1);
		tween.Finished += () => body.OffsetTransformPosition = Vector2.Zero;
	}
}

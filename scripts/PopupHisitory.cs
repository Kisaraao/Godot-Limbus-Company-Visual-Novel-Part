using Godot;

public partial class PopupHisitory : PopupPanel
{
	[Export] public VBoxContainer record_list;
	[Export] public PackedScene scene_record;
	[Export] public AudioStreamPlayer voice;
	[Export] public Button btn_confirm;
	[Export] public AudioUI UI_sound;
	[Export] public ScrollContainer scroll;
	public void add_record(DialogueRecord data)
	{
		Record rc = scene_record.Instantiate<Record>();
		rc.data = data;
		rc.voice = voice;
		rc.UI_sound = UI_sound;
		record_list.AddChild(rc);
	}

	public void _on_confirm_pressed()
	{
		Hide();
	}

	public void _on_confirm_mouse_entered()
	{
		UI_sound.playSound("hover");
		btn_confirm.Modulate = new Color(0.8f, 0.8f, 0.8f);
	}

	public void _on_confirm_mouse_exited()
	{
		btn_confirm.Modulate = Colors.White;
	}
}

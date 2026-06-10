using Godot;

public partial class Speaker : Control
{
	[Export] public TextureRect background;
	[Export] public TextureRect badge;
	[Export] public Label title;
	[Export] public Label speaker;
	public void set_speaker(Dialogue current)
	{
		title.Text = current.mute_speaker ? current.title_mute_speaker : current.speaker.title;
		speaker.Text = current.mute_speaker ? current.name_mute_speaker : current.speaker.name;

		speaker.Modulate = current.speaker.name_color;
		badge.Modulate = current.speaker.badge_color;
		
		Visible = !current.hide_speaker;
	}
}

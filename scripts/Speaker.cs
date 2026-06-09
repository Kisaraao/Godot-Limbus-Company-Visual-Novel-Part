using Godot;

public partial class Speaker : Label
{
	[Export] public Label title;
	[Export] public TextureRect speaker_badge;
	[Export] public TextureRect speaker_label;
	public void set_speaker(Dialogue current)
	{
		title.Text = current.mute_speaker ? current.title_mute_speaker : current.speaker.title;
		Text = current.mute_speaker ? current.name_mute_speaker : current.speaker.name;

		Modulate = current.speaker.name_color;
		speaker_badge.SelfModulate = current.speaker.badge_color;
		
		if (current.hide_speaker)
		{
			Hide();
			speaker_badge.Hide();
			speaker_label.Hide();
			title.Hide();
		}
		else
		{
			Show();
			speaker_badge.Show();
			speaker_label.Show();
			title.Show();
		}
	}
}

using Godot;

[GlobalClass]
public partial class Record : VBoxContainer
{
	public DialogueRecord data;
	[Export] public Label content;
	[Export] public Label name;
	[Export] public TextureRect portrait;
	[Export] public ColorRect background;
	[Export] public AudioStreamPlayer voice;
	[Export] public Button btn_voice;
	[Export] public AudioManager audio;
	public override void _Ready()
	{
		content.Text = data.content;
		name.Text = data.speaker.name;
		background.Color = data.speaker.badge_color;
		portrait.Texture = data.speaker.textures[0].texture;
		portrait.Position = data.speaker.portrait_offset;
		btn_voice.Visible = data.voice != null;
	}

	public void _on_button_pressed()
	{
		if (data.voice == null) return;
		voice.Stop();
		voice.Stream = data.voice;
		voice.Play();
	}

	public void _on_button_mouse_entered()
	{
		audio.playSound("hover");
	}
}

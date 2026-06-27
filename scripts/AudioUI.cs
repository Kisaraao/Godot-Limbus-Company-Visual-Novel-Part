using Godot;
using Godot.Collections;

public partial class AudioUI : AudioStreamPlayer
{
	public Dictionary sound_map;
	public override void _Ready()
	{
		sound_map = new Dictionary
		{
			{ "click", ResourceLoader.Load<AudioStream>("res://audio/sound/click.mp3") },
			{ "hover", ResourceLoader.Load<AudioStream>("res://audio/sound/hover.mp3") },
		};
	}

	public void playSound(string name)
	{
		if (sound_map.TryGetValue(name, out Variant audio))
		{
			Stop();
			Stream = audio.As<AudioStream>();
			Play();
		}
		else
		{
			GD.Print("ERROR Unexpected audio sound.");
		}
	}
}

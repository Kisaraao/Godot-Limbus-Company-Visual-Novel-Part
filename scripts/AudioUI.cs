using System.Collections.Generic;
using Godot;

public partial class AudioUI : AudioStreamPlayer
{
	public Dictionary<string, AudioStream> sound_map;
	public override void _Ready()
	{
		sound_map = new Dictionary<string, AudioStream>();
		sound_map["click"] = ResourceLoader.Load<AudioStream>("res://audio/sound/click.mp3");
		sound_map["hover"] = ResourceLoader.Load<AudioStream>("res://audio/sound/hover.mp3");
	}

	public void playSound(string name)
	{
		if (sound_map.TryGetValue(name, out AudioStream audio))
		{
			Stop();
			Stream = audio;
			Play();
		}
		else
		{
			GD.Print("ERROR Unexpected audio sound.");
		}
	}
}

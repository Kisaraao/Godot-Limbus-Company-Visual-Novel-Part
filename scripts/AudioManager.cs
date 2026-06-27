using Godot;

public partial class AudioManager : CanvasLayer
{
	[Export] public AudioStreamPlayer voice;
	[Export] public AudioStreamPlayer bgm;
	[Export] public AudioStreamPlayer sound;
	[Export] public AudioUI ui;
	
	[Export] public float total_mul;
	[Export] public float bgm_mul;
	[Export] public float sound_mul;
	[Export] public float voice_mul;

	public void set_audio(Dialogue current)
	{
		voice.Stream = current.voice == null ? null : current.voice;
		if (voice.Stream != null) voice.Play();

		sound.Stream = current.sound == null ? null : current.sound;
		if (sound.Stream != null) sound.Play();
		
		if (current.bgm != null)
		{
			bgm.Stop();
			bgm.Stream = current.bgm == null ? null : current.bgm;
			bgm.Play();
		}
	}

	public void playSound(string name)
	{
		ui.playSound(name);
	}

	public void updateVolume()
	{
		bgm.VolumeLinear = bgm_mul * total_mul;
		sound.VolumeLinear = sound_mul * total_mul;
		ui.VolumeLinear = sound_mul * total_mul;
		voice.VolumeLinear = voice_mul * total_mul;
	}
}

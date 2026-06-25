using Godot;

public partial class AudioManager : CanvasLayer
{
	[Export] public AudioStreamPlayer voice;
	[Export] public AudioStreamPlayer bgm;
	[Export] public AudioStreamPlayer sound;
	[Export] public AudioUI ui;
	
	public float total_mul;
	public float bgm_mul;
	public float sound_mul;
	public float voice_mul;

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
		bgm.VolumeDb = 80 * bgm_mul * total_mul - 80;
		sound.VolumeDb = 80 * sound_mul * total_mul - 80;
		voice.VolumeDb = 80 * voice_mul * total_mul - 80;
		ui.VolumeDb = 80 * total_mul - 80;
	}
}

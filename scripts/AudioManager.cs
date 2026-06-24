using Godot;

public partial class AudioManager : CanvasLayer
{
	[Export] AudioStreamPlayer voice;
	[Export] AudioStreamPlayer bgm;
	[Export] AudioStreamPlayer sound;
	[Export] AudioUI ui;
	
	public float total_mul = 1.0f;
	public float bgm_mul = 0.5f;
	public float sound_mul = 1.0f;
	public float voice_mul = 0.75f;

    public override void _Ready()
    {
        updateVolume();
    }


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
		bgm.VolumeDb = 40 * bgm_mul * total_mul - 40;
		sound.VolumeDb = 40 * sound_mul * total_mul - 40;
		voice.VolumeDb = 40 * voice_mul * total_mul - 40;
		ui.VolumeDb = 40 * total_mul - 40;
	}
}

using Godot;

public partial class AudioManager : CanvasLayer
{
	[Export] AudioStreamPlayer voice;
	[Export] AudioStreamPlayer bgm;
	[Export] AudioStreamPlayer sound;
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
}

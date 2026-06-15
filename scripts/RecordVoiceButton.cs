using Godot;
public partial class RecordVoiceButton : Button
{
	public void _on_mouse_entered()
	{
		Modulate = new Color(0.8f, 0.8f, 0.8f);
	}

	public void _on_mouse_exited()
	{
		Modulate = Colors.White;
	}
}

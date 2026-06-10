using Godot;

public partial class ButtonMenu : Button
{
	public void _on_toggled(bool is_toggled)
	{
		Modulate = is_toggled ? new Color(1.572f, 1.572f, 1.572f) : Colors.White;
	}
}

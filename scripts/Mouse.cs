using Godot;

public partial class Mouse : TextureRect
{
    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Hidden;
    }

	public override void _Process(double delta)
	{
		GlobalPosition = GetGlobalMousePosition();
		Modulate = Input.IsActionPressed("mouse_pressing") ? new Color(1.2f, 1.2f ,1.2f) : Colors.White;
	}
}

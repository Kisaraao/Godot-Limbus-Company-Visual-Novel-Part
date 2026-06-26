using Godot;

public partial class test : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public float speed = 500.0f;
	public Vector2 dir = Vector2.Zero;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		dir = Input.GetVector("left", "right", "up", "down");

		Position += dir * speed * (float)delta;
	}
}

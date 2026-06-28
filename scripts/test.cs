using Godot;

public partial class test : Camera2D
{

	[Export] Sprite2D floor;

	public float speed = 500.0f;
	public Vector2 dir = Vector2.Zero;

	public float screen_width;
	public float screen_height;

    public override void _Ready()
    {
		screen_width = GetViewport().GetWindow().Size.X;
		screen_height = GetViewport().GetWindow().Size.Y;
        Position = new Vector2(screen_width / 2, screen_height / 2);
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		dir = Input.GetVector("left", "right", "up", "down");

		Position += dir * speed * (float)delta;

		
		GD.Print(floor.Texture.GetSize());

		var floor_pos = (GetViewport().GetVisibleRect().Size - floor.Texture.GetSize() * floor.Scale) / 2;
		
		// floor_pos.Y = GetViewport().GetVisibleRect().Size.Y - floor.Texture.GetSize().Y * floor.Scale.Y;
		floor_pos.Y = Position.Y;

		floor.Position = floor_pos;
		var a = GetViewport().GetWindow().Size / 2 / Position;
		floor.Scale = new Vector2(floor.Scale.X, Mathf.Clamp(a.Y, 0.1f, 1.0f));
		// floor.Scale += new Vector2(0, dir.Y * speed / 10 * (float)delta);
		// floor.Scale = new Vector2(floor.Scale.X, Mathf.Clamp(floor.Scale.Y, 0.0f, 1.0f));
		// floor.Position = (floor.Texture.GetSize() - Position) / 2;
	}
}

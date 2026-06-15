using Godot;

public partial class CharacterRuntime : Sprite2D
{
	public Character_Instance data;
	private bool shaking = false;
	private int shake_strength = 0;
	public override void _Ready()
	{
		GD.Randomize();

		Vector2 offset = new Vector2();
		foreach (var t in data.character.textures)
		{
			if(t.emotion == data.emotion)
			{
				Texture = t.texture;
				Scale = data.offset_scale == Vector2.Zero ? t.scale + data.character.tex_common_scale : data.offset_scale;
				offset = t.offset;
				break;
			}
		}

		float screen_width = GetViewport().GetVisibleRect().Size.X;

		Position = new Vector2(screen_width / 2 + (int)(screen_width * data.X_index * 0.05) + data.offset.X + offset.X + data.character.tex_common_offset.X, data.character.tex_common_offset.Y + data.offset.Y + offset.Y);

		if (data.gray) Modulate = new Color("#3d3d3d");
		if (data.black) Modulate = Colors.Black;

		if (data.fade_in)
		{
			var fade_tween = GetTree().CreateTween();
			fade_tween.TweenProperty(this, "modulate:a", 1.0f, data.fade_in_duration);
		}

		if (data.pos_animate)
		{
			Position = new Vector2(screen_width / 2 + (int)(screen_width * data.begin_pos_index_animate * 0.05) + data.offset.X + offset.X, data.offset.Y + offset.Y);
			var animate_tween = GetTree().CreateTween();
			animate_tween.SetEase(data.pos_animate_ease_type);
			animate_tween.TweenProperty(this, "position:x", screen_width / 2 + (int)(screen_width * data.end_pos_index_animate * 0.05) + data.offset.X + offset.X, data.pos_animate_duration);
		}

		if (data.shake)
		{
			shake(data.shake_strength, data.shake_duration);
		}
	}

    public override void _Process(double delta)
    {
		if (shaking)
		{
			Offset = new Vector2(GD.RandRange(-shake_strength, shake_strength), GD.RandRange(-shake_strength, shake_strength));
			Offset = new Vector2(
				Mathf.Clamp(Offset.X, -shake_strength, shake_strength),
				Mathf.Clamp(Offset.Y, -shake_strength, shake_strength)
				);
		}
		else
		{
			Offset = Vector2.Zero;
		}
    }

	public void shake(int strength, float duration)
	{
		shake_strength = strength;
		var timer = GetTree().CreateTimer(duration);
		timer.Timeout += stop_shake;
		shaking = true;
	}

	public void stop_shake()
	{
		shaking = false;
		shake_strength = 0;
		var tween = GetTree().CreateTween();
		tween.SetEase(Tween.EaseType.OutIn);
		tween.TweenProperty(this, "offset", Vector2.Zero, 0.1f);
	}

	public void clear()
	{
		if (data.fade_out)
		{
			var fade_tween = GetTree().CreateTween();
			Modulate = new Color("#ffffff");
			fade_tween.TweenProperty(this, "modulate:a", 0.0f, data.fade_out_duration);
			fade_tween.Finished += QueueFree;
		}
		else
		{
			QueueFree();
		}
	}

}

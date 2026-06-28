using Godot;

public partial class BattleBegin : Control
{

	[Export] TextureRect bg_0; 
	[Export] TextureRect bg_1; 

	[Export] AtlasTexture tex_bar;

	[Export] Control icon_control;
	[Export] Control bar_control;

	public bool is_rolling = false;
	public float bg_0_rotation_speed = 0.75f;

	public bool is_playing = false;

	public override void _Ready()
	{
		Show();
		is_playing = true;
		icon_control.Hide();
		show_bar(new Vector2(0, 450), 1000.0f, 600.0f);
	}

	public override void _Process(double delta)
	{
		if (is_rolling) bg_0.Rotation += bg_0_rotation_speed * (float)delta;
	}

	public void show_bar(Vector2 o, float begin_radius, float target_radius)
	{
		float PI = 3.14159f;
		float begin_angle = -PI / 2;

		float l = 3000.0f;

		for (int i = 0; i < 7; i++)
		{
			Vector2 begin_pos = new Vector2(o.X + begin_radius * Mathf.Cos(begin_angle + i * 2 * PI / 7), o.Y + begin_radius * Mathf.Sin(begin_angle + i * 2 * PI / 7));
			Vector2 target_pos = new Vector2(o.X + target_radius * Mathf.Cos(begin_angle + i * 2 * PI / 7), o.Y + target_radius * Mathf.Sin(begin_angle + i * 2 * PI / 7));

			Vector2 at = target_pos - begin_pos;
			
			float angle = Mathf.Atan2(at.Y, at.X) - Mathf.RadToDeg(85);

			begin_pos = new Vector2(
				target_pos.X + l * Mathf.Cos(angle),
				target_pos.Y + l * Mathf.Sin(angle)
			);

			TextureRect bar = new TextureRect
			{
				Texture = tex_bar,
				Position = begin_pos,
				Rotation = Mathf.Atan2(at.Y, at.X) - Mathf.RadToDeg(85),
				PivotOffsetRatio = new Vector2(0.5f, 0.5f),
				Scale = new Vector2(1.5f, 1.5f)
			};
			bar_control.AddChild(bar);
			bar.Name = i.ToString();

			var tween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out).TweenProperty(bar, "position", target_pos, 1.0f).SetDelay((7 - i) * 0.1);
			if (i == 6)
			{
				tween.Finished += () =>
				{
					GetTree().CreateTimer(0.45f).Timeout += show_icon;
				};
			}
		}
	}

	public void show_icon()
	{
		icon_control.Scale = new Vector2(2.5f, 2.5f);
		icon_control.Modulate = new Color("#ffffff00");
		icon_control.Show();
		is_rolling = true;
		var tween = GetTree().CreateTween().SetTrans(Tween.TransitionType.Quad).SetEase(Tween.EaseType.Out);
		tween.SetParallel(true);
		tween.TweenProperty(icon_control, "scale", new Vector2(1.0f, 1.0f), 0.3f);
		tween.TweenProperty(icon_control, "modulate:a", 1.0f, 0.25f);
		tween.TweenProperty(bar_control, "modulate:a", 0.0f, 0.25f);
		tween.Finished += exit;
	}

	public void exit()
	{
		GetTree().CreateTween().TweenProperty(icon_control, "modulate:a", 0.0f, 0.4f).SetDelay(1.25f);
		GetTree().CreateTween().TweenProperty(this, "modulate:a", 0.0f, 0.2f).SetDelay(1.75f).Finished += () => {
			is_playing = false;
			MouseFilter = MouseFilterEnum.Ignore;
		};
	}
}

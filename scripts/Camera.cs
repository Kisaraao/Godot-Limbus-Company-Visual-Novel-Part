using System;
using Godot;

[GlobalClass]
public partial class Camera : Camera2D
{

	private bool shaking = false;

	private int shake_strength = 0;
	private double total_time = 0;

    public override void _Ready()
    {
		GD.Randomize();
    }

    public override void _Process(double delta)
    {
		if(!shaking) return;

		total_time += delta;

		Offset = new Vector2(GD.RandRange(-shake_strength, shake_strength), GD.RandRange(-shake_strength, shake_strength));
		Offset = new Vector2(
			 Mathf.Clamp(Offset.X, -shake_strength, shake_strength),
			 Mathf.Clamp(Offset.Y, -shake_strength, shake_strength)
			);
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
		tween.TweenProperty(this, "offset", new Vector2(0, 0), 0.1f);
	}
}

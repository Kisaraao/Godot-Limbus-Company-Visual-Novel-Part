using System;
using Godot;

[GlobalClass]
public partial class Camera : Camera2D
{

	private bool shaking = false;
	private int shake_strength = 0;

    public override void _Ready()
    {
		GD.Randomize();
    }

    public override void _Process(double delta)
    {
		if(!shaking) return;

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

	public void set_camera(Dialogue current)
	{
		var screen_width = GetViewport().GetVisibleRect().Size.X;
		var screen_height = GetViewport().GetVisibleRect().Size.Y;

		if (current.camera_shake)
		{
			shake(current.camera_shake_strength, current.camera_shake_duration);
		}

		if (current.camera_zoom_smoothing)
		{
			var camera_zoom_tween = GetTree().CreateTween();
			camera_zoom_tween.TweenProperty(this, "zoom", current.camera_zoom, current.duration_camera_zoom_smoothing);
		}
		else
		{
			Zoom = current.camera_zoom;
		}
		
		PositionSmoothingEnabled = current.camera_pos_smoothing;
		RotationSmoothingEnabled = current.camera_rotation_smoothing;
		
		PositionSmoothingSpeed = current.speed_camera_pos_smoothing;
		RotationSmoothingSpeed = current.speed_camera_rotation_smoothing;

		Position = new Vector2((screen_width + current.camera_pos.X) / 2, (screen_height + current.camera_pos.Y) / 2);
		RotationDegrees = current.camera_rotation;
	}
}

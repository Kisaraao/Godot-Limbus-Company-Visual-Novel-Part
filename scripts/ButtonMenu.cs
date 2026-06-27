using Godot;

public partial class ButtonMenu : Button
{
	[Export] public Button btn_skip;
	[Export] public Button btn_autoplay;
	[Export] public Button btn_history;
	[Export] public Button btn_hide_ui;
	[Export] public Button btn_setting;
	[Export] public TextureRect bar;
	[Export] public TextureRect hover;
	public Tween hover_animate;
	public Tween bar_animate;
	public Tween btn_animate;
	[Export] public AudioUI UI_sound;
	public bool shrinking = false;

	public void shrink_bar(float begin_scale_x, float target_scale_x, float duration)
	{
		if (IsInstanceValid(bar_animate)) bar_animate.Kill();

		bar_animate = GetTree().CreateTween();
		bar_animate.SetParallel(true);

		bar.Scale = new Vector2(begin_scale_x, bar.Scale.Y);

		bar_animate.TweenProperty(bar, "scale:x", target_scale_x, duration);
	}

	public void _on_toggled(bool is_toggled)
	{
		UI_sound.playSound("click");
		if (shrinking) return;

		shrinking = true;
		Modulate = is_toggled ? new Color(1.572f, 1.572f, 1.572f) : Colors.White;

		if (IsInstanceValid(btn_animate)) btn_animate.Kill();
		btn_animate = GetTree().CreateTween();
		btn_animate.SetParallel(true);

		if (is_toggled)
		{
			btn_show(btn_setting);
			btn_show(btn_hide_ui);
			btn_show(btn_history);
			btn_show(btn_autoplay);
			btn_show(btn_skip);
		}
		else
		{
			btn_hide(btn_setting);
			btn_hide(btn_hide_ui);
			btn_hide(btn_history);
			btn_hide(btn_autoplay);
			btn_hide(btn_skip);
		}

		if (is_toggled)
		{
			shrink_bar(-0.15f, -0.75f, 0.1f);
		}
		else
		{
			shrink_bar(-0.75f, -0.15f, 0.1f);
		}

		btn_animate.Finished += () => shrinking = false;
	}

	public void btn_show(Button btn)
	{
		var origin_pos_x = btn.Position.X;
		btn.Position = new Vector2(origin_pos_x + 35, btn.Position.Y);
		Color a_color = btn.Modulate;
		a_color.A = 0.0f;
		btn.Modulate = a_color;
		btn.Show();
		btn_animate.TweenProperty(btn, "position:x", origin_pos_x, 0.1);
		btn_animate.TweenProperty(btn, "modulate:a", 1.0f, 0.1);
	}

	public void btn_hide(Button btn)
	{
		var origin_pos = btn.Position;
		btn_animate.SetParallel(true);
		btn_animate.TweenProperty(btn, "position:x", btn.Position.X + 35, 0.1);
		btn_animate.TweenProperty(btn, "modulate:a", 0.0f, 0.1);
		btn_animate.Finished += btn.Hide;
		btn_animate.Finished += () => btn.Position = origin_pos;
	}

	public void _on_button_autoplay_toggled(bool is_toggled)
	{
		UI_sound.playSound("click");
		btn_autoplay.Modulate = is_toggled ? new Color(1.9f, 1.9f, 1.9f, 1.0f) : Colors.White;
	}

	public void _show_hover()
	{
		UI_sound.playSound("hover");
		if (IsInstanceValid(hover_animate)) hover_animate.Kill();
		hover_animate = GetTree().CreateTween();
		hover_animate.TweenProperty(hover, "modulate:a", 1.0, 0.2);
	}

	public void _on_this_exited()
	{
		if (IsInstanceValid(hover_animate)) hover_animate.Kill();
		hover_animate = GetTree().CreateTween();
		hover_animate.TweenProperty(hover, "modulate:a", 0.0, 0.2);
	}

	public void _on_mouse_exited()
	{
		if (shrinking) return;
		if (IsInstanceValid(hover_animate)) hover_animate.Kill();
		hover_animate = GetTree().CreateTween();
		hover_animate.TweenProperty(hover, "modulate:a", 0.0, 0.2);
	}
	public void _on_mouse_entered()
	{
		hover.Position = Position;
		_show_hover();
	}
	public void _on_button_setting_mouse_entered()
	{
		if (shrinking) return;
		hover.Position = btn_setting.Position;
		_show_hover();
	}
	public void _on_button_hide_ui_mouse_entered()
	{
		if (shrinking) return;
		hover.Position = btn_hide_ui.Position;
		_show_hover();
	}
	public void _on_button_history_mouse_entered()
	{
		if (shrinking) return;
		hover.Position = btn_history.Position;
		_show_hover();
	}
	public void _on_button_autoplay_mouse_entered()
	{
		if (shrinking) return;
		hover.Position = btn_autoplay.Position;
		_show_hover();
	}
	public void _on_button_skip_mouse_entered()
	{
		if (shrinking) return;
		hover.Position = btn_skip.Position;
		_show_hover();
	}
}

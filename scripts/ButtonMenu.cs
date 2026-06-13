using Godot;

public partial class ButtonMenu : Button
{
	[Export] public Button btn_skip;
	[Export] public Button btn_autoplay;
	[Export] public Button btn_history;
	[Export] public Button btn_hide_ui;
	[Export] public Button btn_config;
	[Export] public TextureRect bar;

	public bool shrinking = false;

	public void _on_toggled(bool is_toggled)
	{
		if (shrinking) return;

		shrinking = true;
		Modulate = is_toggled ? new Color(1.572f, 1.572f, 1.572f) : Colors.White;
		// btn_skip.Visible = is_toggled;
		// btn_autoplay.Visible = is_toggled;
		// btn_history.Visible = is_toggled;
		// btn_hide_ui.Visible = is_toggled;
		// btn_config.Visible = is_toggled;

		if (is_toggled)
		{
			btn_show(btn_config);
			btn_show(btn_hide_ui);
			btn_show(btn_history);
			btn_show(btn_autoplay);
			btn_show(btn_skip);
		}
		else
		{
			btn_hide(btn_config);
			btn_hide(btn_hide_ui);
			btn_hide(btn_history);
			btn_hide(btn_autoplay);
			btn_hide(btn_skip);
		}

		if (is_toggled)
		{
			var bar_animate_tween = GetTree().CreateTween();
			bar_animate_tween.SetParallel(true);
			bar_animate_tween.TweenProperty(bar, "position:x", 1303, 0.1f);
			bar_animate_tween.TweenProperty(bar, "size:x", 528, 0.1f);
		}
		else
		{
			var bar_animate_tween = GetTree().CreateTween();
			bar_animate_tween.SetParallel(true);
			bar_animate_tween.TweenProperty(bar, "position:x", 1787, 0.1f);
			bar_animate_tween.TweenProperty(bar, "size:x", 44, 0.1f);
		}

		shrinking = false;
	}

	public void btn_show(Button btn)
	{
		var btn_show_tween = GetTree().CreateTween();
		btn_show_tween.SetParallel(true);
		var origin_pos_x = btn.Position.X;
		btn.Position = new Vector2(origin_pos_x + 35, btn.Position.Y);
		btn.Modulate = new Color("#ffffff00");
		btn.Show();
		btn_show_tween.TweenProperty(btn, "position:x", origin_pos_x, 0.1);
		btn_show_tween.TweenProperty(btn, "modulate:a", 1.0f, 0.1);
	}

	public void btn_hide(Button btn)
	{
		var origin_pos = btn.Position;
		var btn_hide_tween = GetTree().CreateTween();
		btn_hide_tween.SetParallel(true);
		btn_hide_tween.TweenProperty(btn, "position:x", btn.Position.X + 35, 0.1);
		btn_hide_tween.TweenProperty(btn, "modulate:a", 0.0f, 0.1);
		btn_hide_tween.Finished += btn.Hide;
		btn_hide_tween.Finished += () => btn.Position = origin_pos;
	}

	public void _on_button_autoplay_toggled(bool is_toggled)
	{
		btn_autoplay.Modulate = is_toggled ? new Color(1.9f, 1.9f, 1.9f, 1.0f) : Colors.White;
	}
}

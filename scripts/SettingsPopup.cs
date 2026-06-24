using Godot;

public partial class SettingsPopup : PopupPanel
{
	[Export] AudioManager audio;
	[Export] public HSlider total;
	[Export] public HSlider bgm;
	[Export] public HSlider sound;
	[Export] public HSlider voice;
	[Export] public Button btn_confirm;
	[Export] public Button btn_default;
	[Export] public Control body;

	public void _on_total_drag_ended(bool value_changed)
	{
		if (!value_changed) return;
		audio.total_mul = (float)total.Value;
		GD.Print(total.Value);
	}
	
	public void _on_bgm_drag_ended(bool value_changed)
	{
		if (!value_changed) return;
		audio.bgm_mul = (float)bgm.Value;
		audio.updateVolume();
	}

	public void _on_sound_drag_ended(bool value_changed)
	{
		if (!value_changed) return;
		audio.sound_mul = (float)sound.Value;
		audio.updateVolume();
	}

	public void _on_voice_drag_ended(bool value_changed)
	{
		if (!value_changed) return;
		audio.voice_mul = (float)voice.Value;
		audio.updateVolume();
	}

	public void _on_total_button_toggled(bool is_toggle)
	{
		audio.total_mul = is_toggle ? 0.0f : (float)total.Value;
		audio.updateVolume();
	}

	public void _on_bgm_button_toggled(bool is_toggle)
	{
		audio.bgm_mul = is_toggle ? 0.0f : (float)bgm.Value;
		audio.updateVolume();
	}

	public void _on_sound_button_toggled(bool is_toggle)
	{
		audio.sound_mul = is_toggle ? 0.0f : (float)sound.Value;
		audio.updateVolume();
	}

	public void _on_voice_button_toggled(bool is_toggle)
	{
		audio.voice_mul = is_toggle ? 0.0f : (float)voice.Value;
		audio.updateVolume();
	}

	public void _on_confirm_pressed()
	{
		Hide();
	}

	public void _on_confirm_mouse_entered()
	{
		audio.playSound("hover");
		btn_confirm.Modulate = new Color(0.8f, 0.8f, 0.8f);
	}

	public void _on_confirm_mouse_exited()
	{
		btn_confirm.Modulate = Colors.White;
	}

	public void _on_default_pressed()
	{
		total.Value = 1.0f;
		audio.total_mul = 1.0f;
		
		bgm.Value = 0.5f;
		audio.bgm_mul = 0.5f;
		
		sound.Value = 1.0f;
		audio.sound_mul = 1.0f;

		voice.Value = 0.75f;
		audio.voice_mul = 0.75f;
		
		audio.updateVolume();
	}

	public void _on_default_mouse_entered()
	{
		audio.playSound("hover");
		btn_default.Modulate = new Color(0.8f, 0.8f, 0.8f);
	}

	public void _on_default_mouse_exited()
	{
		btn_default.Modulate = Colors.White;
	}

	public void _on_about_to_popup()
	{
		body.OffsetTransformPosition = new Vector2(0, -15);
		var tween = GetTree().CreateTween();
		tween.TweenProperty(body, "offset_transform_position", Vector2.Zero, 0.1);
		tween.Finished += () => body.OffsetTransformPosition = Vector2.Zero;
	}
}

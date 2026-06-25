using Godot;

public partial class PopupSkip : PopupPanel
{
	[Export] public Control body;
	[Export] public GameManager manager;
	[Export] public AudioManager audio;

	[Export] public Button btn_confirm;
	[Export] public Button btn_cancel;

	public void _on_about_to_popup()
	{
		body.OffsetTransformPosition = new Vector2(0, -15);
		var tween = GetTree().CreateTween();
		tween.TweenProperty(body, "offset_transform_position", Vector2.Zero, 0.1);
		tween.Finished += () => body.OffsetTransformPosition = Vector2.Zero;
	}

	public void _on_confirm_pressed()
	{
		manager._skip();
	}

	public void _on_cancel_pressed()
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

	public void _on_cancel_mouse_entered()
	{
		audio.playSound("hover");
		btn_cancel.Modulate = new Color(0.8f, 0.8f, 0.8f);
	}
	public void _on_cancel_mouse_exited()
	{
		btn_cancel.Modulate = Colors.White;
	}
}

using Godot;

public partial class Filter : ColorRect
{
	public Tween fade_tween;
    public override void _Ready()
    {
		fade_tween = GetTree().CreateTween();
		fade_tween.Stop();
    }

	public void fade(Color begin_color, Color end_color, float duration)
	{
		Color = begin_color;
		fade_tween.Stop();
		fade_tween.Kill();
		fade_tween = GetTree().CreateTween();
		fade_tween.SetEase(Tween.EaseType.InOut);
		fade_tween.TweenProperty(this, "color", end_color, duration);
	}
}

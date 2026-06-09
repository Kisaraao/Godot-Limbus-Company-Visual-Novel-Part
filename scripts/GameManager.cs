using Godot;
using Godot.Collections;

public partial class GameManager : CanvasLayer
{
	[Export] Camera camera;
	[Export] ColorRect pure_color_filter;
	[Export] Content content;
	[Export] Speaker speaker;
	[Export] PlaceContent place_name;
	[Export] CanvasLayer character_layer;
	[Export] AudioManager audio;
	[Export] Story story;
	private int index = -1;

	public override void _Ready()
	{
		switch_dialogue();
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionJustReleased("next_dialogue"))
		{
			switch_dialogue();
		}
	}

	public void switch_dialogue(){
		// end typing
		if (content.typing)
		{
			content.end_typing();
			return;
		}

		camera.stop_shake();

		// on end fade
		if (index != -1 && story.dialogues[index].fade_end)
		{
			fade(story.dialogues[index].fade_end_start_color, story.dialogues[index].fade_end_target_color, story.dialogues[index].fade_end_duration);
		}

		if (index + 1 < story.dialogues.Count) index++; else return;

		if (index < story.dialogues.Count)
		{
			var current = story.dialogues[index];

			// on end fade
			if (current.fade_begin)
			{
				fade(current.fade_begin_start_color, current.fade_begin_target_color, current.fade_begin_duration);
			}

			clear_character();
			add_character(current);
			content.set_content(current);
			place_name.set_place(current);
			speaker.set_speaker(current);
			audio.set_audio(current);
			camera.set_camera(current);
		}
	}

	public void clear_character()
	{
		foreach (CharacterRuntime son in character_layer.GetChildren())
		{
			son.clear();
		}
	}

	public void add_character(Dialogue current)
	{
		foreach (var ch in current.characters)
		{
			CharacterRuntime character = new CharacterRuntime();
			character.data = ch;
			character_layer.AddChild(character);
		}
	}

	public void fade(Color begin_color, Color end_color, float duration)
	{
		pure_color_filter.Color = begin_color;
		var fade_tween = GetTree().CreateTween();
		fade_tween.SetEase(Tween.EaseType.InOut);
		fade_tween.TweenProperty(pure_color_filter, "color", end_color, duration);
	}
}

using Godot;
using Godot.Collections;

public partial class GameManager : CanvasLayer
{
	[Export] Camera camera;
	[Export] ColorRect pure_color_filter;
	[Export] Content content;
	[Export] Speaker speaker;
	[Export] PlaceContent place_name;
	[Export] Control buttons;
	[Export] CanvasLayer character_layer;
	[Export] AudioManager audio;
	[Export] Story story;
	private int index = -1;
	private bool switch_cool_down = false;
	private float cool_down_time = 0.5f;
	private bool lose_focus = false;
	private bool hide_ui = false;

	[Export] public PopupHisitory history;
	[Export] public Timer timer_cool_down;

	public override void _Ready()
	{
		var begin_timer = GetTree().CreateTimer(0.1);
		begin_timer.Timeout += switch_dialogue;
		timer_cool_down.Timeout += stop_cool_down;
	}

	public void switch_dialogue(){

		camera.stop_shake();

		// on end fade
		if (index != -1 && story.dialogues[index].fade_end)
		{
			fade(story.dialogues[index].fade_end_start_color, story.dialogues[index].fade_end_target_color, story.dialogues[index].fade_end_duration);
		}

		if (index + 1 < story.dialogues.Count && !switch_cool_down) index++; else return;

		if (index < story.dialogues.Count)
		{
			var current = story.dialogues[index];

			history.add_record(new DialogueRecord(story.dialogues[index].speaker, story.dialogues[index].content, story.dialogues[index].voice));

			// on end fade
			if (current.fade_begin)
			{
				fade(current.fade_begin_start_color, current.fade_begin_target_color, current.fade_begin_duration);
			}

			clear_character();
			add_character(current);
			place_name.set_place(current);
			speaker.set_speaker(current);
			audio.set_audio(current);
			camera.set_camera(current);
			content.set_content(current);
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
	
	public void _on_control_gui_input(InputEvent e)
	{
		if (Input.IsActionJustReleased("next_dialogue"))
		{
			if (lose_focus) return;
			if (hide_ui)
			{
				_on_button_pressed();
				return;
			}
			if (content.typing)
			{
				content.skip_typing();
				return;
			}
			else if (!switch_cool_down)
			{
				switch_dialogue();
				switch_cool_down = true;
				if (timer_cool_down.IsStopped())
				{
					timer_cool_down.Stop();
					timer_cool_down.WaitTime = 0.5;
				}
				timer_cool_down.Start();
			}
		}
	}

	public void _on_button_pressed()
	{
		hide_ui = !hide_ui;
		place_name.Visible = !hide_ui;
		content.Visible = !hide_ui;
		speaker.Visible = !hide_ui;
		buttons.Visible = !hide_ui;
	}

	public void stop_cool_down()
	{
		switch_cool_down = false;
	}

	public void _on_button_history_pressed()
	{
		lose_focus = true;
		history.Popup();
		pure_color_filter.Color = new Color("#000000b7");
	}

	public void _on_history_popup_hide()
	{
		lose_focus = false;
		pure_color_filter.Color = new Color("#ffffff00");
	}
}

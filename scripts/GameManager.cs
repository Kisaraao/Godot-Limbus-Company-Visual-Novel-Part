using Godot;
using Godot.Collections;

public partial class GameManager : CanvasLayer
{
	[Export] Camera camera;
	[Export] Filter filter;
	[Export] Content content;
	[Export] Speaker speaker;
	[Export] PlaceContent place_name;
	[Export] Control buttons;
	[Export] CharacterManager character_manager;
	[Export] AudioManager audio;
	[Export] Control ui;
	private int index = 0;
	private bool switch_cool_down = false;
	private float cool_down_time = 0.5f;
	private bool lose_focus = false;
	private bool hide_ui = false;
	private bool auto_play = false;
	private double total_time = 0.0;
	private double voice_end_time = -1;
	private double typing_end_time = -1;
	[Export] public PopupHisitory history;
	[Export] public SettingsPopup settings;
	[Export] public PopupSkip skip;
	[Export] public Timer timer_cool_down;
	[Export] Story story;

	public bool is_skip = false;
	[Export] public PackedScene selecting_level_scene;

	public override void _Ready()
	{
		var begin_timer = GetTree().CreateTimer(0.1);
		// begin_timer.Timeout += switch_dialogue;
		timer_cool_down.Timeout += () => switch_cool_down = false;
		audio.voice.Finished += () => { voice_end_time = total_time; };
		content.typing_end += () => { typing_end_time = total_time; };
		ui.GrabFocus();

		// on begin fade
		if (story.dialogues[index].fade_begin)
		{
			if (filter.fade_tween.IsRunning())
			{
				filter.fade_tween.Finished += () => filter.fade(story.dialogues[index].fade_begin_start_color, story.dialogues[index].fade_begin_target_color, story.dialogues[index].fade_begin_duration);
				filter.fade_tween.Finished += () => change_canvas(story.dialogues[index]);
				return;
			}
			else
			{
				filter.fade(story.dialogues[index].fade_begin_start_color, story.dialogues[index].fade_begin_target_color, story.dialogues[index].fade_begin_duration);
			}
		}
		change_canvas(story.dialogues[index]);
	}

    public override void _Process(double delta)
    {
		total_time += delta;

		if (is_skip) return;

		if (index >= story.dialogues.Count) return;
		if (audio.voice.Playing) return;

		double target_time = story.dialogues[index].voice == null ? typing_end_time : voice_end_time; 

		if (target_time < 0.0 || total_time < target_time + 0.8) return;
		if (lose_focus || hide_ui) return;
        if (!auto_play) return;

		typing_end_time = -1;
		voice_end_time = -1;
		_next();
    }


	public void switch_dialogue(){

		if (is_skip) return;

		camera.stop_shake();

		// on end fade
		if (index != 0 && story.dialogues[index].fade_end)
		{
			filter.fade(story.dialogues[index].fade_end_start_color, story.dialogues[index].fade_end_target_color, story.dialogues[index].fade_end_duration);
		}

		if (index <= story.dialogues.Count - 1 && !switch_cool_down) index++; else return;

		if (index <= story.dialogues.Count - 1)
		{
			var current = story.dialogues[index];

			history.add_record(new DialogueRecord(story.dialogues[index].speaker, story.dialogues[index].content, story.dialogues[index].voice));

			// on begin fade
			if (current.fade_begin)
			{
				if (filter.fade_tween.IsRunning())
				{
					filter.fade_tween.Finished += () => filter.fade(current.fade_begin_start_color, current.fade_begin_target_color, current.fade_begin_duration);
					filter.fade_tween.Finished += () => change_canvas(current);
					return;
				}
				else
				{
					filter.fade(current.fade_begin_start_color, current.fade_begin_target_color, current.fade_begin_duration);
				}
			}
			change_canvas(current);
		}
	}

	public void change_canvas(Dialogue current)
	{
		if (is_skip) return;

		character_manager.clear();
		character_manager.add_character(current.characters);
		place_name.set_place(current);
		speaker.set_speaker(current);
		audio.set_audio(current);
		camera.set_camera(current);
		content.set_content(current);
	}
	
	public void _on_control_gui_input(InputEvent e)
	{
		if (is_skip) return;

		if (e.IsActionReleased("next_dialogue"))
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
			
			if (!switch_cool_down)
			{
				audio.playSound("click");
				_next();
			}
		}
	}

	public void _next()
	{
		if (is_skip) return;

		switch_dialogue();
		switch_cool_down = true;
		if (timer_cool_down.IsStopped())
		{
			timer_cool_down.Stop();
			timer_cool_down.WaitTime = 0.5;
		}
		timer_cool_down.Start();
	}

	public void _on_button_pressed()
	{
		if (is_skip) return;
		audio.playSound("click");
		hide_ui = !hide_ui;
		place_name.Visible = !hide_ui;
		content.Visible = !hide_ui;
		speaker.Visible = !hide_ui;
		buttons.Visible = !hide_ui;
	}

	public void _on_button_history_pressed()
	{
		if (is_skip) return;
		audio.playSound("click");
		lose_focus = true;
		history.Popup();
		filter.Color = new Color("#000000b7");
	}

	public void _on_settings_button_config_pressed()
	{
		if (is_skip) return;
		audio.playSound("click");
		lose_focus = true;
		settings.Popup();
		filter.Color = new Color("#000000b7");
	}

	public void _on_button_skip_pressed()
	{
		if (is_skip) return;
		audio.playSound("click");
		lose_focus = true;
		skip.Popup();
		filter.Color = new Color("#000000b7");
	}

	public void _on_history_popup_hide()
	{
		if (is_skip) return;
		audio.playSound("click");
		lose_focus = false;
		filter.Color = new Color("#ffffff00");
	}

	public void _on_settings_popup_hide()
	{
		if (is_skip) return;
		audio.playSound("click");
		lose_focus = false;
		filter.Color = new Color("#ffffff00");
	}

	public void _on_skip_popup_hide()
	{
		if (is_skip) return;
		audio.playSound("click");
		lose_focus = false;
		filter.Color = new Color("#ffffff00");
	}

	public void _on_button_autoplay_toggled(bool is_toggled)
	{
		if (is_skip) return;
		audio.playSound("click");
		auto_play = is_toggled;
	}

	public void _skip()
	{
		is_skip = true;
		CallDeferred(nameof(switch_to_selecting));
	}

	private void switch_to_selecting()
	{
		QueueFree();
		GetTree().ChangeSceneToPacked(selecting_level_scene);
	}
}
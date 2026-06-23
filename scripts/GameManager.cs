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
	private int index = -1;
	private bool switch_cool_down = false;
	private float cool_down_time = 0.5f;
	private bool lose_focus = false;
	private bool hide_ui = false;
	private bool auto_play = false;
	[Export] public PopupHisitory history;
	[Export] public Timer timer_cool_down;
	[Export] Story story;
	public override void _Ready()
	{
		var begin_timer = GetTree().CreateTimer(0.1);
		begin_timer.Timeout += switch_dialogue;
		timer_cool_down.Timeout += () => switch_cool_down = false;
		ui.GrabFocus();
	}

	public void switch_dialogue(){

		camera.stop_shake();

		// on end fade
		if (index != -1 && story.dialogues[index].fade_end)
		{
			filter.fade(story.dialogues[index].fade_end_start_color, story.dialogues[index].fade_end_target_color, story.dialogues[index].fade_end_duration);
		}

		if (index + 1 < story.dialogues.Count && !switch_cool_down) index++; else return;

		if (index < story.dialogues.Count)
		{
			var current = story.dialogues[index];

			history.add_record(new DialogueRecord(story.dialogues[index].speaker, story.dialogues[index].content, story.dialogues[index].voice));

			// on end fade
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
		audio.playSound("click");
		hide_ui = !hide_ui;
		place_name.Visible = !hide_ui;
		content.Visible = !hide_ui;
		speaker.Visible = !hide_ui;
		buttons.Visible = !hide_ui;
	}

	public void _on_button_history_pressed()
	{
		audio.playSound("click");
		lose_focus = true;
		history.Popup();
		filter.Color = new Color("#000000b7");
	}

	public void _on_history_popup_hide()
	{
		audio.playSound("click");
		lose_focus = false;
		filter.Color = new Color("#ffffff00");
	}

	public void _on_button_autoplay_toggled(bool is_toggled)
	{
		audio.playSound("click");
		auto_play = is_toggled;
	}
}
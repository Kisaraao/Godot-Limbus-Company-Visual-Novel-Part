using Godot;
using Godot.Collections;

public partial class GameManager : CanvasLayer
{
	[Export] Camera camera;

	[Export] ColorRect pure_color_filter;

	[Export] TextureRect content_label;
	[Export] Content content;

	// speaker
	[Export] Label title;
	[Export] Label speaker;
	[Export] TextureRect speaker_label;
	[Export] TextureRect speaker_badge;
	[Export] Node2D place;
	[Export] PlaceContent place_name;
	[Export] TextureRect background;
	[Export] AudioStreamPlayer voice;
	[Export] AudioStreamPlayer bgm;
	[Export] AudioStreamPlayer sound;

	[Export] CanvasLayer character_layer;

	[Export] Story story;
	private int index = -1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		switch_dialogue();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
			set_content(current);
			set_place(current);
			set_speaker(current);
			set_audio(current);
			set_camera(current);
		}
	}

	public void clear_character()
	{
		foreach (var son in character_layer.GetChildren())
		{
			son.QueueFree();
		}
	}

	public void set_camera(Dialogue current)
	{
		var screen_width = GetViewport().GetVisibleRect().Size.X;
		var screen_height = GetViewport().GetVisibleRect().Size.Y;

		if (current.camera_shake)
		{
			camera.shake(current.camera_shake_strength, current.camera_shake_duration);
		}

		if (current.camera_zoom_smoothing)
		{
			var camera_zoom_tween = GetTree().CreateTween();
			camera_zoom_tween.TweenProperty(camera, "zoom", current.camera_zoom, current.duration_camera_zoom_smoothing);
		}
		else
		{
			camera.Zoom = current.camera_zoom;
		}
		
		camera.PositionSmoothingEnabled = current.camera_pos_smoothing;
		camera.RotationSmoothingEnabled = current.camera_rotation_smoothing;
		
		camera.PositionSmoothingSpeed = current.speed_camera_pos_smoothing;
		camera.RotationSmoothingSpeed = current.speed_camera_rotation_smoothing;

		camera.Position = new Vector2((screen_width + current.camera_pos.X) / 2, (screen_height + current.camera_pos.Y) / 2);
		camera.RotationDegrees = current.camera_rotation;
	}

	public void add_character(Dialogue current)
	{
		foreach (var ch in current.characters)
		{
			Sprite2D character = new Sprite2D();
			Array<Character_Texture> tex = ch.character.textures;

			Vector2 offset = new Vector2(0, 0);

			foreach (var t in tex)
			{
				if(t.emotion == ch.emotion)
				{
					character.Texture = t.texture;
					character.Scale = t.scale;
					offset = t.offset;
					break;
				}
			}

			float screen_width = GetViewport().GetVisibleRect().Size.X;

			character.Position = new Vector2(screen_width / 2 + (int)(screen_width * ch.X_index) + ch.offset.X, ch.offset.Y + offset.Y);

			if (ch.gray) character.Modulate = new Color("#3d3d3d");
			if (ch.black) character.Modulate = Colors.Black;

			character_layer.AddChild(character);
		}
	}

	public void set_content(Dialogue current)
	{
		content.HorizontalAlignment = current.content_horizontal_alignment;
		content.print_speed = current.print_speed;

		var screen_width = GetViewport().GetVisibleRect().Size.X;
		var screen_height = GetViewport().GetVisibleRect().Size.Y;
		
		content_label.Position = new Vector2(
			(screen_width - content_label.Texture.GetWidth()) / 2 + current.offset_content.X,
			screen_height - content_label.Texture.GetHeight() + current.offset_content.Y
		);

		if (current.hide_content) { content_label.Hide(); } else { content_label.Show(); }

		content.switch_text(current.content);
	}

	public void set_speaker(Dialogue current)
	{
		title.Text = current.mute_speaker ? current.title_mute_speaker : current.speaker.title;
		speaker.Text = current.mute_speaker ? current.name_mute_speaker : current.speaker.name;

		speaker.Modulate = current.speaker.name_color;
		speaker_badge.SelfModulate = current.speaker.badge_color;
		
		if (current.hide_speaker)
		{
			speaker.Hide();
			speaker_badge.Hide();
			speaker_label.Hide();
			title.Hide();
		}
		else
		{
			speaker.Show();
			speaker_badge.Show();
			speaker_label.Show();
			title.Show();
		}
	}

	public void set_place(Dialogue current)
	{
		string name = current.mute_place ? current.name_mute_place : current.place.name;
		place_name.switch_text(name);
		background.Texture = current.place.texture;
		background.Scale = current.background_scale;
		
		var screen_width = GetViewport().GetVisibleRect().Size.X;
		var screen_height = GetViewport().GetVisibleRect().Size.Y;

		background.Position = new Vector2(
			(screen_width - background.Texture.GetWidth() * background.Scale.X) / 2 + current.background_offset.X,
			(screen_height - background.Texture.GetHeight() * background.Scale.Y) / 2 + current.background_offset.Y
			 );

		if (current.hide_place) { place.Hide(); } else { place.Show(); }
	}

	public void set_audio(Dialogue current)
	{
		voice.Stream = current.voice == null ? null : current.voice;
		if (voice.Stream != null) voice.Play();

		sound.Stream = current.sound == null ? null : current.sound;
		if (sound.Stream != null) sound.Play();
		
		if (current.bgm != null)
		{
			bgm.Stop();
			bgm.Stream = current.bgm == null ? null : current.bgm;
			bgm.Play();
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

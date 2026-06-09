using Godot;

public partial class Content : Label
{
	[Export] TextureRect background;
	private string origin_str;
	private int current_char_index = 0;
	public bool typing = false;
	public double total_time = 0;
	public double print_speed = 0.04;

	public override void _Process(double delta)
	{
		if (!typing) return;
		total_time += delta;
		while (total_time >= print_speed && current_char_index < origin_str.Length)
		{
			Text += origin_str[current_char_index++];
			total_time -= print_speed;
		}

		if (current_char_index >= origin_str.Length) typing = false;
	}

	public void switch_text(string str)
	{
		origin_str = str;
		Text = "";
		current_char_index = 0;
		total_time = 0;
		typing = true;
	}

	public void end_typing()
	{
		typing = false;
		Text = origin_str;
		current_char_index = 0;
		total_time = 0;
	}

	public void set_content(Dialogue current)
	{
		HorizontalAlignment = current.content_horizontal_alignment;
		print_speed = current.print_speed;

		var screen_width = GetViewport().GetVisibleRect().Size.X;
		var screen_height = GetViewport().GetVisibleRect().Size.Y;
		
		background.Position = new Vector2(
			(screen_width - background.Texture.GetWidth()) / 2 + current.offset_content.X,
			screen_height - background.Texture.GetHeight() + current.offset_content.Y
		);

		if (current.hide_content) { background.Hide(); } else { background.Show(); }

		switch_text(current.content);
	}
}

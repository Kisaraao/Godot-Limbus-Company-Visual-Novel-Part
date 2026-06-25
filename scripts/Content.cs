using Godot;

public partial class Content : Control
{
	private string origin_str;
	private int current_char_index = 0;
	public bool typing = false;
	public double total_time = 0;
	public double print_speed = 0.04;

	[Export] TextureRect background;
	[Export] Label content;

	public delegate void EventHandler();
	public event EventHandler typing_end;

	public override void _Process(double delta)
	{
		if (!typing) return;
		total_time += delta;
		while (total_time >= print_speed && current_char_index < origin_str.Length)
		{
			content.Text += origin_str[current_char_index++];
			total_time -= print_speed;
		}

		if (current_char_index < origin_str.Length) return;
		
		typing = false;
		typing_end.Invoke();
	}

	public void switch_text(string str)
	{
		origin_str = str;
		content.Text = "";
		current_char_index = 0;
		total_time = 0;
		typing = true;
	}

	public void skip_typing()
	{
		typing = false;
		content.Text = origin_str;
		current_char_index = 0;
		total_time = 0;
	}

	public void set_content(Dialogue current)
	{
		content.HorizontalAlignment = current.content_horizontal_alignment;
		print_speed = current.print_speed;

		var screen_width = GetViewport().GetVisibleRect().Size.X;
		var screen_height = GetViewport().GetVisibleRect().Size.Y;
		
		background.Position = new Vector2(
			(screen_width - background.Texture.GetWidth()) / 2 + current.offset_content.X,
			screen_height - background.Texture.GetHeight() + current.offset_content.Y
		);

		Visible = !current.hide_content;

		switch_text(current.content);
	}
}

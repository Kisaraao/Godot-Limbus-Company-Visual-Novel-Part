using Godot;

public partial class Content : Control
{
	private string origin_str;
	private int current_char_index = 0;
	public bool typing = false;
	public double cool_down_time = 0;
	public double total_time = 0;
	public double print_speed = 0.04;

	[Export] TextureRect background;
	[Export] Label content;
	[Export] TextureRect logo;
	public delegate void EventHandler();
	public event EventHandler typing_end;

    public override void _Ready()
    {
        typing_end += () => {
			logo.Position = content.Position + content.GetCharacterBounds(content.Text.Length - 1).Position + new Vector2(content.GetThemeFontSize("口") + 10, 0);
			logo.Show();
		};
    }

	public override void _Process(double delta)
	{
		if (!typing) return;
		cool_down_time += delta;
		while (cool_down_time >= print_speed && current_char_index < origin_str.Length)
		{
			content.Text += origin_str[current_char_index++];
			cool_down_time -= print_speed;
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
		cool_down_time = 0;
		logo.Hide();
		typing = true;
	}

	public void skip_typing()
	{
		typing = false;
		content.Text = origin_str;
		current_char_index = 0;
		cool_down_time = 0;
		typing_end.Invoke();
	}

	public void set_content(Dialogue current)
	{
		content.HorizontalAlignment = current.content_horizontal_alignment;
		print_speed = current.print_speed;
		
		content.Position += current.offset_content;
		background.Position += current.offset_content;

		Visible = !current.hide_content;

		switch_text(current.content);
	}
}

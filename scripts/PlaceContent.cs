using Godot;

public partial class PlaceContent : Control
{
	public bool typing = false;
	public double total_time = 0;
	public string origin_str;
	public int current_char_index = 0;

	[Export] public TextureRect background;
	[Export] public TextureRect badge;
	[Export] public TextureRect decoration;
	[Export] public Label place_content;

	public override void _Process(double delta)
	{
		if (!typing) return;

		total_time += delta;
		while (total_time >= 0.05 && current_char_index < origin_str.Length)
		{
			place_content.Text += origin_str[current_char_index++];
			total_time -= 0.05;
		}

		if (current_char_index >= origin_str.Length) typing = false;
	}

	public void switch_text(string str)
	{
		if (origin_str == str) return;
		origin_str = str;
		place_content.Text = "";
		current_char_index = 0;
		total_time = 0;
		typing = true;
	}

	public void set_place(Dialogue current)
	{
		string name = current.mute_place ? current.name_mute_place : current.place.name;
		switch_text(name);
		background.Texture = current.place.texture;
		background.Scale = current.background_scale;
		
		var screen_width = GetViewport().GetVisibleRect().Size.X;
		var screen_height = GetViewport().GetVisibleRect().Size.Y;

		background.Position = new Vector2(
			(screen_width - background.Texture.GetWidth() * background.Scale.X) / 2 + current.background_offset.X,
			(screen_height - background.Texture.GetHeight() * background.Scale.Y) / 2 + 15 + current.background_offset.Y
			 );

		Visible = !current.hide_place;
	}
}

using Godot;
using System;

public partial class PlaceContent : Label
{
	public bool typing = false;
	public double total_time = 0;
	public string origin_str;
	public int current_char_index = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!typing) return;

		total_time += delta;
		while (total_time >= 0.05 && current_char_index < origin_str.Length)
		{
			Text += origin_str[current_char_index++];
			total_time -= 0.05;
		}

		if (current_char_index >= origin_str.Length) typing = false;
	}

	public void switch_text(string str)
	{
		if (origin_str == str) return;
		origin_str = str;
		Text = "";
		current_char_index = 0;
		total_time = 0;
		typing = true;
	}
}

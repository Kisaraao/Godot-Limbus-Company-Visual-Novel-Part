using Godot;
using System;
using System.Numerics;

public partial class Content : Label
{
	private string origin_str;
	private int current_char_index = 0;
	public bool typing = false;
	public double total_time = 0;
	public double print_speed = 0.04;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
}

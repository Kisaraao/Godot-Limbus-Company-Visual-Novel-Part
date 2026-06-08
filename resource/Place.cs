using Godot;
using System;

[GlobalClass]
public partial class Place : Resource
{
    [Export] public string name;
    [Export] public Texture2D texture;
}

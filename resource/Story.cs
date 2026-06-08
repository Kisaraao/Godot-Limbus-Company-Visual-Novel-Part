using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Story : Resource
{
    [Export] public string name;
    [Export] public Array<Dialogue> dialogues;
}

using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Story : Resource
{
    [Export] public string name;
    [Export] public Array<Dialogue> dialogues;
}

using Godot;
using Godot.Collections;

public partial class CharacterManager : CanvasLayer
{
	public void clear()
	{
		foreach (CharacterRuntime son in GetChildren())
		{
			son.clear();
		}
	}

	public void add_character(Array<Character_Instance> characters)
	{
		foreach (var ch in characters)
		{
			CharacterRuntime character = new CharacterRuntime { data = ch };
			AddChild(character);
		}
	}
}

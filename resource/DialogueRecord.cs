using Godot;

[GlobalClass]
public partial class DialogueRecord : Resource
{
    public DialogueRecord(Character speaker, string content)
    {
        this.speaker = speaker;
        this.content = content;
    }
    public Character speaker;
    public string content;
}

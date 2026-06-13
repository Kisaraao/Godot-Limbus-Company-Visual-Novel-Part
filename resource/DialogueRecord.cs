using Godot;

[GlobalClass]
public partial class DialogueRecord : Resource
{
    public DialogueRecord(Character speaker, string content, AudioStream voice)
    {
        this.speaker = speaker;
        this.content = content;
        this.voice = voice;
    }
    public Character speaker;
    public string content;
    public AudioStream voice;
}

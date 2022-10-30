using Godot;
using System;

public class StartMenu : Node
{
    public void _onClick(){
        GetTree().ChangeScene("res://Level/Level.tscn");
    }
}

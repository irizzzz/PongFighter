using Godot;
using System;

public class Level : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private PackedScene _balls = (PackedScene)GD.Load("res://Ball/Ball.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void noBalls(KinematicBody2D body){
        if(GetTree().GetNodesInGroup("Balls").Count == 1){
             var b = (Ball)_balls.Instance<Ball>();
             b.restart();
             this.CallDeferred("add_child", b);
        }
    }
}

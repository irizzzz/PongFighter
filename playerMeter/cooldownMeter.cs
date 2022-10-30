using Godot;
using System;

public class cooldownMeter : TextureProgress
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public int player_id;
    public Timer shootTimer;
    public Timer hitTimer;


    public float percentageLeft;

    // Called when the node enters the scene tree for the first time.

    public void setup(int id, Timer shoot, Timer hit){
        this.player_id = id;
        this.shootTimer = shoot;
        this.hitTimer = hit;
    }

    public override void _Process(float delta)
    {
        if(shootTimer.TimeLeft > 0){
            this.percentageLeft = 100 - (1 - this.shootTimer.TimeLeft/this.shootTimer.WaitTime)*100;
            this.Value = percentageLeft;
        } else if(this.Value > 0) {
            this.percentageLeft = 100 - (1 - this.hitTimer.TimeLeft/this.hitTimer.WaitTime)*100;
            this.Value = percentageLeft;
        }
        
    }
}

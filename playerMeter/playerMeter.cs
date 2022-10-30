using Godot;
using System;

public class playerMeter : TextureProgress
{

    // Called when the node enters the scene tree for the first time.
    public void _on_PassiveGainTimer_timeout(){
        this.Value+=0.5;
    }

    public void incrMeter(){
        this.Value+=2.5;
    }
}

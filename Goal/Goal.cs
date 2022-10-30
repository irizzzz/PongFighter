using Godot;
using System;

public class Goal : Area2D
{
    [Export]
    public int goal_id = 1;
    int p1Score = 0;
    int p2Score = 0;
    Label p1Label;
    Label p2Label;
    
    public ProgressBar grd;

    public TextureProgress meter_1;
    public TextureProgress meter_2;


    public override void _Ready()
    {
        p1Label = GetNode<Label>("/root/Level/P1Score");
        p2Label = GetNode<Label>("/root/Level/P2Score");
        meter_1 = GetNode<TextureProgress>("/root/Level/P1Meter");
        meter_2 = GetNode<TextureProgress>("/root/Level/P2Meter");


    }

    public void OnWallAreaEntered(KinematicBody2D body){
        if(body is Ball ball){
            ball.reset();
            if(goal_id == 1){
                p2Score+=1;
                p2Label.Text= p2Score.ToString();
                meter_2.Value+=5;
                meter_1.Value-=5;
            } else if(goal_id == 2){
                p1Score+=1;
                p1Label.Text= p1Score.ToString();
                meter_1.Value+=5;
                meter_2.Value-=5;

            }
        }
   }
}

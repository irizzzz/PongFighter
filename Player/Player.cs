using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int mvSpeed = 500;
    [Export]
    public int player_id = 1;
    private PackedScene _balls = (PackedScene)GD.Load("res://Ball/Ball.tscn");
    [Export]
    public int stocks = 0;

    public TextureProgress meter;

    public TextureProgress cooldown;

    public Timer shootTimer;
    public Timer hitTimer;
    public Timer dashTimer;


    public override void _Ready(){
        meter = GetNode<TextureProgress>($"/root/Level/P{player_id}Meter");
        meter.Value = 25;
        cooldown = GetNode<TextureProgress>($"/root/Level/P{player_id}Cooldown");
        shootTimer = GetNode<Timer>($"/root/Level/P{player_id}CooldownTimer");
        hitTimer = GetNode<Timer>($"/root/Level/P{player_id}HitTimer");
        dashTimer = GetNode<Timer>($"/root/Level/P{player_id}DashTimer");
        cooldown.Call("setup", player_id, shootTimer, hitTimer);
        shootTimer.WaitTime = (float)1.5;
        hitTimer.WaitTime = (float)0.9;
        dashTimer.WaitTime = (float)0.05;

    }

    public override void _Input(InputEvent inputEvent){
        if(inputEvent.IsActionPressed($"shoot_{player_id}") && meter.Value >= 25 && cooldown.Value == 0){
            meter.Value-=25;
            spawnBall();
            cooldown.Value = 100;
            shootTimer.Start();
        }

        
    }


    public override void _PhysicsProcess(float delta){
        var motion = new Vector2();
        if(Input.IsActionJustPressed($"dash_{player_id}") && meter.Value > 12.5 
        && (Input.IsActionPressed($"move_down_{player_id}") || Input.IsActionPressed($"move_up_{player_id}"))){
            dashTimer.Start();
            meter.Value -= 12.5;
            mvSpeed = 2000;
        }

        if(dashTimer.TimeLeft == 0){
            mvSpeed = 700;
        }
        motion.y = Input.GetActionStrength($"move_down_{player_id}") - Input.GetActionStrength($"move_up_{player_id}");
        MoveAndCollide(motion.Normalized() * mvSpeed * delta);

       
    }

    public void firstHit(KinematicBody2D body){
        if(body is Ball Ball){
            Ball.velocity = new Vector2(Ball.velocity.x,((float)new Random().NextDouble()) * 2 - 1).Normalized();
        }
    }

    public void spawnBall(){
            var b = (Ball)_balls.Instance<Ball>();
            b.shoot(GetNode<KinematicBody2D>($"/root/Level/Player{player_id}").GlobalPosition, player_id);
            GetParent().AddChild(b);
    }

    public void playerHit(){
        cooldown.Value = 100;
        hitTimer.Start();
        meter.Call("incrMeter");
    }
}

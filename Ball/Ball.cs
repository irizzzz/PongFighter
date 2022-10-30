using Godot;
using System;

public class Ball : KinematicBody2D
{
    [Export]
    public int speed = 400;
    [Export]
    public Vector2 velocity = new Vector2();
    private bool firstColl = true; 
    //private Vector2 initialPos;
    

    
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        AddToGroup("Balls");
        if(Input.IsActionJustPressed($"shoot_1")){
            velocity = Vector2.Right;
        } else if(Input.IsActionJustPressed($"shoot_2")){
            velocity = Vector2.Left;

        } 

        else {
            if(random.RandiRange(1, 10) >= 5){
            velocity = Vector2.Left;
             } else {
            velocity = Vector2.Right;
            }
        }
        
    }

    public override void _PhysicsProcess(float delta)
    {
        var collision = MoveAndCollide(velocity.Normalized() * speed * delta);
        if(collision != null){
            if(this.velocity.y > 0.9 || this.velocity.y < -0.9){
                this.velocity = new Vector2(this.velocity.x,((float)new Random().NextDouble()) * 2 - 1).Normalized();
            }
            velocity = velocity.Bounce(collision.Normal);
            if(collision.Collider.HasMethod("firstHit") && firstColl == true){
                firstColl = false;
                collision.Collider.Call("firstHit", this);
            } else if(collision.Collider.HasMethod("ballHit")){
                collision.Collider.Call("ballHit", this);
            }

            if(collision.Collider.HasMethod("playerHit")){
                collision.Collider.Call("playerHit");
            }
        }

    }

    public void shoot(Vector2 p, int id){
        if(id == 1){
            p.x+=40;
        } else if(id == 2) {
            p.x-=40;

        }
        Position = p;
        velocity.x = 1;
        velocity.y = 0;
    }

    
    public void reset(){
        this.RemoveFromGroup("Balls");
        QueueFree();
    }

    public void restart(){
        Position = new Vector2(640, 360);
    }
    
    

    public void ballHit(KinematicBody2D body){
        if(body is Ball Ball){
            Ball.velocity = new Vector2(Ball.velocity.x,((float)new Random().NextDouble()) * 2 - 1).Normalized();
        }
    }
    
}

using Godot;

public partial class ProjectileBullet : Bullet {
    [Export(PropertyHint.Range, "0,3000,1")] int Speed = 1750;
    Vector2 RotationVector;

    public override void _Ready() {
        RotationVector = new Vector2(1.0f, 0.0f).Rotated(Rotation);
    }
    
    public override void _PhysicsProcess(double delta) {
        Velocity = Speed * RotationVector;
        MoveAndSlide();
    }
}
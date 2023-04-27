using Godot;

public partial class HomingBullet : Bullet {
    [Export(PropertyHint.Range, "0,3000,1")] float Speed = 1750.0f;
    Node2D Target;
    Vector2 DeltaPos;

    public override void _Ready() {
        Godot.Collections.Array<Node> Enemies = GetTree().GetNodesInGroup("Enemies");
        Target = (Node2D)Enemies[0];
        foreach (Node2D _Node in Enemies) {
            Target = (_Node.Position.DistanceTo(Position) < Target.Position.DistanceTo(Position)) ? _Node : Target;
        }
    }
    
    public override void _PhysicsProcess(double delta) {
        DeltaPos = Target.Position-Position;
        Rotation = Mathf.LerpAngle(Rotation, Mathf.Atan2(DeltaPos.Y, DeltaPos.X), (float)(6.0f*delta));
        Velocity = Speed* new Vector2(1.0f, 0.0f).Rotated(Rotation);
        MoveAndSlide();
    }
}
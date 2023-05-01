using Godot;

public partial class InstantBullet : Bullet {
    public override void _Ready() {
        base._Ready();
        RayCast2D Ray = GetNode<RayCast2D>("RayCast2D");
        Line2D Line = GetNode<Line2D>("Line2D");

        Ray.TargetPosition = new Vector2(MaxDistance, 0.0f).Rotated(Rotation);
        Ray.ForceRaycastUpdate();
        HitPos = Ray.GetCollisionPoint();
        Line.SetPointPosition(1, HitPos);
    }
}
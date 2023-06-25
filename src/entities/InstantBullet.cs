using Godot;

public partial class InstantBullet : Bullet {
	[Export] RayCast2D Ray;
	[Export] Line2D Line;
	 
	public override void _Ready() {
		base._Ready();

		Ray.TargetPosition = new Vector2(MaxDistance, 0.0f).Rotated(Rotation);
		Ray.ForceRaycastUpdate();
		HitPos = Ray.GetCollisionPoint();
		Line.SetPointPosition(1, HitPos);
	}
}
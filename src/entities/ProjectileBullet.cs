using Godot;

public partial class ProjectileBullet : Bullet {
	[Export(PropertyHint.Range, "0,3000,1")] float Speed = 1750.0f;
	Vector2 RotationVector;
	KinematicCollision2D Collision;
	GodotObject Collider;

	public override void _Ready() {
		base._Ready();
		RotationVector = Vector2.Right.Rotated(Rotation);
	}
	
	public override void _PhysicsProcess(double delta) {
		Velocity = Speed * RotationVector;
		Collision = MoveAndCollide(Velocity * (float)delta);
		if (Collision == null) return;

		Collider = Collision.GetCollider();
		if (!(Collider is Enemy)) return;
		
		((Enemy)Collider).Harm(Damage);
		HitPos = GlobalPosition;
		Despawn();
	}
}

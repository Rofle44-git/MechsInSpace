using Godot;

public partial class Enemy : CharacterBody2D {
	CharacterBody2D Target;
	const int Speed = 350;
	Vector2 DeltaPos;

	public override void _Ready() {
		Target = GetParent().GetNode<CharacterBody2D>("Player");
	}

	public override void _PhysicsProcess(double delta) {
		DeltaPos = Target.Position-Position;
		Rotation = Mathf.Atan2(DeltaPos.Y, DeltaPos.X);
		Velocity = Speed * new Vector2(1.0f, 0.0f).Rotated(Rotation);
		MoveAndSlide();
	}
}
